using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.GridBrushBase;

public class CharacterController : MonoBehaviour
{
    [Header("Movement stats")]
    public float moveSpeed;
    public float accelerationSpeed;
    public float maxSpeed;
    public Vector2 moveInput; // Input from controller.

    [Header("Rotation stats")]
    public float playerAngle;
    public Vector3 targetRotation;
    public Vector3 meshTargetRotation;
    public float rotationSpeed;

    [Header("Actions stats")]
    public bool canBark;

    [Header("LayerCheck stats")]
    [SerializeField] GameObject borderCheck;
    [SerializeField] float borderCheckRadius;
    [SerializeField] GameObject layerCheck;
    [SerializeField] float layerCheckRadius;
    // Layers:
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask slopeLayer;
    // Bools:
    [SerializeField] bool groundAhead;
    [SerializeField] bool slopeAhead;
    [SerializeField] bool isOnSlope;

    [Header("References")]
    public Rigidbody playerRb;
    public GameObject playerMesh;
    public GameObject barkArea;
    public GameObject worldAxsis;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerMesh = GameObject.Find("PlayerMesh");
        barkArea = GameObject.Find("BarkArea");
        layerCheck = GameObject.Find("LayerCheck");
        borderCheck = GameObject.Find("BorderCheck");
        worldAxsis = GameObject.Find("PF_WorldAxsis");
    }

    private void Start()
    {
        barkArea.SetActive(false);

        canBark = true;
    }

    private void Update()
    {
        CheckUpdate();
    }

    private void FixedUpdate()
    {
        PlayerRotation();

        PlayerMove();

        //MeshRotation();
    }

    private void CheckUpdate()
    {
        groundAhead = Physics.CheckSphere(borderCheck.transform.position, borderCheckRadius, groundLayer);
        slopeAhead = Physics.CheckSphere(borderCheck.transform.position, borderCheckRadius, slopeLayer);

        isOnSlope = Physics.CheckSphere(layerCheck.transform.position, layerCheckRadius, slopeLayer);
    }

    private void PlayerRotation()
    {
        // Defines where should the player rotate.
        targetRotation = new Vector3
        (
            transform.eulerAngles.x,
            playerAngle + worldAxsis.transform.eulerAngles.y, // Adds the camera rotation.
            transform.eulerAngles.z
        );

        // Rotates the player.
        if (moveInput != new Vector2(0, 0))
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
        if (moveInput != new Vector2(0, 0) && (groundAhead || slopeAhead)) // Accelerates the player when it starts moving (and there's ground/slope).
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
        else if (moveSpeed > 0) // Deaccelerates the player when it stops moving.
        {
            moveSpeed -= accelerationSpeed * 2;
        }
        else if (moveSpeed != 0) // Prevents the player from moving while still.
        {
            moveSpeed = 0;
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime; // Moves the player forward.   
    }

    private void MeshRotation()
    {
        // Defines where should the player mesh rotate.
        if (isOnSlope)
        {
            meshTargetRotation = new Vector3
            (
                -45,
                playerMesh.transform.eulerAngles.y,
                playerMesh.transform.eulerAngles.z
            );
        }
        else
        {
            meshTargetRotation = new Vector3
            (
                0,
                playerMesh.transform.eulerAngles.y,
                playerMesh.transform.eulerAngles.z
            );
        }

        // Rotates the player mesh.
        playerMesh.transform.rotation = Quaternion.RotateTowards
        (
            playerMesh.transform.rotation,
            Quaternion.Euler(meshTargetRotation),
            rotationSpeed * Time.deltaTime
        );

        // lee la rotacion del jugador de 0 a 1. Restale 0.5 y multiplicalo por 45*2. Usa el resultado en la rotación del mesh.
    }

    private void Bark()
    {
        if (canBark)
        {
            canBark = false;
            barkArea.SetActive(true);
            barkArea.SetActive(false);
            canBark = true;
            Debug.Log("I barked :)");
        }
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
        Bark();
    }

    public void OnDig(InputAction.CallbackContext context)
    {

    }

    #endregion
}
