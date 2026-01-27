using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static ScenesManager;
using static UnityEngine.GridBrushBase;

public class PlayerController_2 : MonoBehaviour
{
    [Header("Movement stats")]
    [SerializeField] bool canMove;
    [SerializeField] float moveSpeed;
    [SerializeField] float accelerationSpeed;
    private float maxSpeed;
    [SerializeField] Vector2 moveInput; // Input from controller.

    [Header("Rotation stats")]
    [SerializeField] float playerAngle;
    [SerializeField] Vector3 targetRotation;
    [SerializeField] Vector3 meshTargetRotation;
    [SerializeField] float rotationSpeed;

    [Header("Actions stats")]
    public bool canBark;
    public bool canDig;

    [Header("Border stats")]
    [SerializeField] GameObject borderCheckA;
    [SerializeField] GameObject borderCheckB;
    [SerializeField] GameObject borderCheckLeft;
    [SerializeField] GameObject borderCheckRight;
    [SerializeField] float borderCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask coverLayer;
    [SerializeField] bool groundAhead;
    [SerializeField] bool groundAtLeft;
    [SerializeField] bool groundAtRight;

    [Header("Animator")]
    [SerializeField] GameObject SK_Braska;
    [SerializeField] Animator playerAnim;

    [Header("Object references")]
    [SerializeField] Rigidbody playerRb;
    [SerializeField] GameObject playerMesh;
    [SerializeField] GameObject areaBark;
    [SerializeField] GameObject areaDig;
    public ParticleSystem barkParticles;
    private GameObject worldAxsis;
    [SerializeField] GameObject orb;
    public ParticleSystem trackParticles;

    [Header("VFX")]
    [SerializeField] GameObject DigVFX;

    [Header("Footprints")]
    [SerializeField] GameObject footprintPrefab;
    [SerializeField] float stepDistance = 0.6f;
    private Vector3 lastFootprintPos;

    [Header("Player Glow")]
    [SerializeField] private Renderer playerRenderer; // Mesh del jugador
    [SerializeField] public Color normalGlow = Color.black; // Color cuando no está ladrando
    [SerializeField] public Color barkGlow = Color.cyan; // Color del glow al ladrar
    [SerializeField] private float glowIntensity = 5f;
    [Header("Player Glow Fade")]
    private bool isGlowing = false;
    private float glowTimer = 0f;
    private float glowDuration = 1f; 
    private Color currentGlowColor;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = SK_Braska.GetComponent<Animator>();

        worldAxsis = GameObject.Find("PF_WorldAxsis");
        orb = GameObject.Find("PF_Orb");

        if (trackParticles != null)
        {
            trackParticles.Stop();
        }
        barkParticles.Stop();
    }

    private void Start()
    {
        lastFootprintPos = transform.position;

        HidePlayerAtStart();

        Invoke(nameof(ShowPlayerAfterDelay), 1f);

        if (trackParticles != null)
        {
            trackParticles.Stop();
        }
        barkParticles.Stop();

        areaBark.SetActive(false);
        areaDig.SetActive(false);

        SpawnTransform();

        if (SceneManager.GetActiveScene().name == "SCN_Level0" && ScenesManager.instance.collectedOrbs == -1)
        {
            ObjectManager.instance.hasOrb = true;
        }
        else
        {
            ObjectManager.instance.hasOrb = false;
        }

        playerAnim.SetBool("isBarking", false);

        if (playerRenderer != null)
        {
            playerRenderer.material.EnableKeyword("_EMISSION");
            playerRenderer.material.SetColor("_EmissionColor", normalGlow);
        }
    }

    private void SpawnTransform()
    {
        // Spawns the player where the teleport is.
        if (ScenesManager.instance.SpawnTeleport != "")
        {
            transform.SetPositionAndRotation(new Vector3(
                GameObject.Find(ScenesManager.instance.SpawnTeleport).transform.position.x,
                GameObject.Find(ScenesManager.instance.SpawnTeleport).transform.position.y + 0.5f,
                GameObject.Find(ScenesManager.instance.SpawnTeleport).transform.position.z
            ), GameObject.Find(ScenesManager.instance.SpawnTeleport).transform.rotation);
        }
    }

    private void Update()
    {
        if (GameState.IsPaused) return;
        CheckUpdate();
        UpdateGlowFade();
    }

    private void CheckUpdate() // Updates all terrain checks.
    {
        groundAhead =
            Physics.CheckSphere(borderCheckA.transform.position, borderCheckRadius, groundLayer)
            && Physics.CheckSphere(borderCheckB.transform.position, borderCheckRadius, groundLayer);

        groundAtLeft = Physics.CheckSphere(borderCheckLeft.transform.position, borderCheckRadius, groundLayer);
        groundAtRight = Physics.CheckSphere(borderCheckRight.transform.position, borderCheckRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        PlayerRotation();

        PlayerMove();

        // Adds artificial gravity to the player
        playerRb.AddForce(new Vector3(0, -20, 0));
    }

    private void HidePlayerAtStart()
    {

        canMove = false;
        canBark = false;
        canDig = false;

        moveSpeed = 0f;
        if (playerRb != null)
        {
            playerRb.angularVelocity = Vector3.zero;
        }

        if (SK_Braska != null)
            SK_Braska.SetActive(false);

        if (playerAnim != null)
            playerAnim.SetBool("isBarking", false);

        if (areaBark != null)
            areaBark.SetActive(false);

    }

    private void ShowPlayerAfterDelay()
    {

        if (ScenesFade.Instance != null)
        {
            ScenesFade.Instance.PlayTeleportVFX(transform.position);
        }

        if (SK_Braska != null)
            SK_Braska.SetActive(true);

        canMove = true;
        canBark = true;
        canDig = true;



        if (playerAnim != null)
            playerAnim.SetBool("isBarking", false);


    }

    private void PlayerRotation()
    {
        // Defines where should the player rotate.
        targetRotation = new Vector3(
            transform.eulerAngles.x,
            playerAngle + worldAxsis.transform.eulerAngles.y, // Adds the camera rotation.
            transform.eulerAngles.z
        );

        // Rotates the player.
        if (moveInput != new Vector2(0, 0) && canMove)
        {
            transform.rotation = Quaternion.RotateTowards
            (
                transform.rotation,
                Quaternion.Euler(targetRotation),
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void PlayerMove()
    {
        // Reduces the max speed if the player is going down a slope.
        if (playerMesh.transform.eulerAngles.x + (playerMesh.transform.eulerAngles.x > 300 ? -360 : 0) > 20)
        {
            maxSpeed = 1.5f;
        }
        else
        {
            maxSpeed = 2.25f;
        }

        if (moveInput != Vector2.zero && canMove && groundAhead) // Accelerates the player when it can and starts moving (and there's ground/slope).
        {
            // Prevents the player from going too fast.
            if (moveSpeed <= maxSpeed)
            {
                moveSpeed += accelerationSpeed;
            }
            else if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
        else if (!groundAhead) // Stops and decelerates the player when there is not terrain ahead.
        {
            moveSpeed = 0;
        }
        else if (moveSpeed > 0) // Decelerates the player when it stops moving.
        {
            moveSpeed -= accelerationSpeed * 2f;
        }
        else if (moveSpeed != 0) // Completely stops the player from moving while still.
        {
            moveSpeed = 0;

            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        // If there is a cliff at one of the sides, reduces the player speed and moves it to the other side.
        if (!groundAtLeft)
        {
            moveSpeed *= 0.85f;
            transform.position += +moveSpeed * Time.deltaTime * transform.right;
        }
        if (!groundAtRight)
        {
            moveSpeed *= 0.85f;
            transform.position += -moveSpeed * Time.deltaTime * transform.right;
        }

        transform.position += moveSpeed * Time.deltaTime * transform.forward; // Moves the player forward.
        TrySpawnFootprint();

    }
    void TrySpawnFootprint()
    {
        if (!canMove || moveInput == Vector2.zero || !groundAhead)
            return;

        if (Vector3.Distance(transform.position, lastFootprintPos) < stepDistance)
            return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f, coverLayer))
        {
            GameObject fp = Instantiate(
            footprintPrefab,
             hit.point + Vector3.up * 0.01f,
          Quaternion.LookRotation(transform.forward),
              hit.transform
            );
            fp.transform.SetParent(hit.transform, false);
       

           
            Vector3 parentScale = hit.transform.lossyScale;
            fp.transform.localScale = new Vector3(
                1f / parentScale.x,
                1f / parentScale.y,
                1f / parentScale.z
            );
           
            lastFootprintPos = transform.position;
        }
    }
    private void LateUpdate()
    {
        Animator();
    }

    private void Animator()
    {
        if (moveInput != Vector2.zero && canMove && groundAhead)
        {
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }

        playerAnim.SetFloat(
            "tiltLevel",
            playerMesh.transform.eulerAngles.x + (playerMesh.transform.eulerAngles.x > 300 ? -360 : 0)
        );
    }

    private void StartBark()
    {

        if (GameState.IsPaused) return;
        if (canBark && canDig)
        {
            if (ObjectManager.instance.barkAvailable)
            {
                barkParticles.Play();

                int[] barkSFXIndices = new int[] { 7, 8, 1 };

                // Elegir uno aleatoriamente
                int randomIndex = Random.Range(0, barkSFXIndices.Length);
                int sfxIndex = barkSFXIndices[randomIndex];

                // Reproducir el SFX
                AudioManager.Instance.PlaySFX(sfxIndex);
            }

            canBark = false;
            canDig = false;
            canMove = false;
            areaBark.SetActive(true);

            //if (ObjectManager.instance.barkAvailable)
                playerAnim.SetBool("isBarking", true);

            Invoke(nameof(FinishAction), 0.5f);

            isGlowing = true;
            glowTimer = 0f;
            currentGlowColor = normalGlow;

        }
    }
    private void UpdateGlowFade()
    {
        if (!isGlowing || playerRenderer == null) return;

        glowTimer += Time.deltaTime;
        float halfDuration = glowDuration / 2f;

        if (glowTimer <= halfDuration)
        {
            float t = glowTimer / halfDuration;
            foreach (Material mat in playerRenderer.materials)
            {
                mat.SetColor("_EmissionColor", Color.Lerp(normalGlow, barkGlow * glowIntensity, t));
            }
        }
        else if (glowTimer <= glowDuration)
        {
            float t = (glowTimer - halfDuration) / halfDuration;
            foreach (Material mat in playerRenderer.materials)
            {
                mat.SetColor("_EmissionColor", Color.Lerp(barkGlow * glowIntensity, normalGlow, t));
            }
        }
        else
        {
            // Termina el glow
            foreach (Material mat in playerRenderer.materials)
            {
                mat.SetColor("_EmissionColor", normalGlow);
            }
            isGlowing = false;
        }
    }

    private void StartDig()
    {
        if (GameState.IsPaused) return;
        if (canDig && canBark)
        {
            canBark = false;
            canDig = false;
            canMove = false;
            areaDig.SetActive(true);

            ObjectManager.instance.LocateOrb();

            Invoke(nameof(StartTrack), 0.1f);
        }
    }

    private void StartTrack()
    {
        if (trackParticles != null && !ObjectManager.instance.hasOrb)
        {
            trackParticles.Play();
            AudioManager.Instance.PlaySFX(5);
        }

        if (ObjectManager.instance.hasOrb)
        {
            playerAnim.SetBool("isDigging", true);
        }
        else
        {
            playerAnim.SetBool("isTracking", true);
        }

        Invoke(nameof(FinishAction), 1.5f);
    }

    private void FinishAction()
    {
        areaBark.SetActive(false);
        areaDig.SetActive(false);
        canMove = true;
        canBark = true;
        canDig = true;

        playerAnim.SetBool("isBarking", false);
        playerAnim.SetBool("isTracking", false);
        playerAnim.SetBool("isDigging", false);
    }

    #region Input Methods
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (!context.canceled)
        {
            playerAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg; // Transform the input vector 2 into a float .        
        }
    }

    public void OnBark(InputAction.CallbackContext context)
    {
        StartBark();
    }

    public void OnDig(InputAction.CallbackContext context)
    {
        if (!ObjectManager.instance.hasOrb)
        {
            StartDig();
        } 
    }
    #endregion
}
