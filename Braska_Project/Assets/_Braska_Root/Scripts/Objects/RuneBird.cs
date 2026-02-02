using Unity.VisualScripting;
using UnityEngine;

public class RuneBird : MonoBehaviour
{
    [Header("a")]
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform pointC;

    [SerializeField] float speed = 2f;

    Vector3[] path;
    int currentIndex = 0;
    int direction = 1;
   public bool moving = false;
    public RuneTriggerBird currentRune;
    public bool waitingForBark = true;
    public Animator animator;
    public float delay = 3f;
    public float delayTiempo = 1f;
    public float delayTiempoladrar = 1f;
    public float LandingTime = 0.75f;
    public float delayTiempoladrar2 = 0.15f;

    [Header("Look Targets")]
    [SerializeField] Transform lookWhileFlying;
    [SerializeField] Transform lookIdleAtA;
    [SerializeField] Transform lookIdleAtC;

    [Header("Glow on Flight")]
    [SerializeField] Renderer birdRenderer; 
    [SerializeField] Color emissiveColor = Color.yellow;  
    [SerializeField] float glowIntensity = 10f;  
    [SerializeField] float glowFadeSpeed = 2f;  
    private Material birdMaterial;
    private Color originalEmissionColor;
    private bool isGlowing = false;

    [Header("Dust Effect")]
    [SerializeField] private GameObject dustPrefab;
    [SerializeField] private float dustSpacing = 0.5f; 
    private Vector3 lastDustPos;

    void Start()
    {

        path = new Vector3[] { pointA.position, pointB.position, pointC.position };
        transform.position = path[0]; // empieza en A
        currentIndex = 0;

        if (animator != null)
        {
            animator.SetBool("IsFlying", false);
            animator.SetBool("IsLanding", false);
            animator.SetBool("Idle", true);
            animator.SetBool("TakeOff", false);
        }
        if (birdRenderer != null)
        {
            birdMaterial = birdRenderer.material;
            originalEmissionColor = birdMaterial.GetColor("_EmissionColor");
            birdMaterial.EnableKeyword("_EMISSION"); 
        }
    }

    void Update()
    {
        if (!moving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            path[currentIndex],
            speed * Time.deltaTime
        );
        TrySpawnDust();

        if (Vector3.Distance(transform.position, path[currentIndex]) < 0.01f)
        {
            ArrivedAtPoint();
        }
        if (isGlowing && birdMaterial != null)
        {
            Color current = birdMaterial.GetColor("_EmissionColor");
            Color target = originalEmissionColor;
            birdMaterial.SetColor("_EmissionColor", Color.Lerp(current, target, glowFadeSpeed * Time.deltaTime));

            if (Vector4.Distance(current, target) < 0.01f)
            {
                isGlowing = false;
            }
        }
    }
    void TrySpawnDust()
    {
        if (dustPrefab == null) return;

        if (lastDustPos == Vector3.zero)
        {
            lastDustPos = transform.position;
            return;
        }

        float distance = Vector3.Distance(transform.position, lastDustPos);
        if (distance >= dustSpacing)
        {
            GameObject dust = Instantiate(
                dustPrefab,
                transform.position,
                Quaternion.identity
            );

            Destroy(dust, 5f);

            lastDustPos = transform.position;
        }
    }


    void ArrivedAtPoint()
    {
        if (currentIndex == 0 || currentIndex == 2)
        {
            moving = false;

            Invoke(nameof(HandleArrival), delayTiempo);

           

            return;
        }

        if (currentIndex == 1)
        {
            currentIndex += direction;
        }
     }
    void HandleArrival()
    {
        Invoke(nameof(HandleArrival2), LandingTime);
        

        // Detecta runes cerca
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hit in hits)
        {
            RuneTriggerBird rune = hit.GetComponent<RuneTriggerBird>();
            if (rune != null)
            {
                currentRune = rune;
                break;
            }
        }
    }
    void HandleArrival2()
    {
        Invoke(nameof(barking), delayTiempoladrar);

        if (animator != null)
        {
            animator.SetBool("IsFlying", false);
            animator.SetBool("IsLanding", true);
            animator.SetBool("Idle", false);
            animator.SetBool("TakeOff", false);
        }
    }
    void barking ()
    {
        waitingForBark = true;
    }
    public Transform GetLookTarget()
    {
        if (moving)
            return lookWhileFlying;

        if (!moving && currentIndex == 0)
            return lookIdleAtA;
        if (!moving && currentIndex == 2)
            return lookIdleAtC;

        return null;
    }

    public void StartMove()
    {
        
        if (!waitingForBark) return;

        waitingForBark = false;
        moving = true;

        if (currentIndex == 0) direction = 1;
        else if (currentIndex == 2) direction = -1;

        if (currentIndex == 0) currentIndex = 1;
        else if (currentIndex == 2) currentIndex = 1;
        Invoke(nameof(VolarAnimación), delayTiempoladrar);

        if (animator != null)
        {
            animator.SetBool("IsFlying", false);
            animator.SetBool("TakeOff", true);
            animator.SetBool("IsLanding", false);
            animator.SetBool("Idle", false);
        }
        Invoke(nameof(VolarAnimación), delayTiempoladrar2);
        ActivateGlow();
    }
    void VolarAnimación()
    {
        animator.SetBool("IsFlying", true);
        animator.SetBool("IsLanding", false);
        animator.SetBool("Idle", false);
        animator.SetBool("TakeOff", false);
    }
    void ActivateGlow()
    {
        if (birdMaterial == null) return;
        birdMaterial.SetColor("_EmissionColor", emissiveColor * glowIntensity);
        isGlowing = true;
    }
}