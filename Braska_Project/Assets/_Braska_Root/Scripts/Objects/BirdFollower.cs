using UnityEngine;

public class BirdFollower : MonoBehaviour
{
    [Header("References")]
    public RuneBird targetBird;
    public float followDelay = 0.5f;
    public float speed = 20f;
    [SerializeField] float rotationSpeedFlying = 1f; // velocidad normal al volar
    [SerializeField] float rotationSpeedIdle = 1f;  // grados/segundo cuando está en A o C

    public bool isActive = false;
    public bool playersee = false;
    public float currentIntensity;
    public float targetIntensity;
    public Renderer pedestalRenderer;
    public Renderer pedestalRenderer2;
    private Material runeMaterial;
    private Material runeMaterial2;
    [Header("Emission Settings")]
    [SerializeField] float baseEmissionIntensity = 4f;
    [SerializeField] float highlightMultiplier = 1.5f;

    public float timemove = 4.5f;

    public Color glowColor = Color.cyan;


    private Vector3 lastTargetPosition;

    void Start()
    {
        if (targetBird != null)
            lastTargetPosition = targetBird.transform.position;

        if (pedestalRenderer == null || pedestalRenderer2 == null) return;

        runeMaterial = new Material(pedestalRenderer.material);
        pedestalRenderer.material = runeMaterial;
        runeMaterial.EnableKeyword("_EMISSION");

        runeMaterial2 = new Material(pedestalRenderer2.material);
        pedestalRenderer2.material = runeMaterial2;
        runeMaterial2.EnableKeyword("_EMISSION");

        ApplyBaseEmission();

        currentIntensity = baseEmissionIntensity;
        targetIntensity = baseEmissionIntensity;
    }

    void Update()
    {
        if (targetBird == null) return;

        // Calcula la posición atrasada del pájaro principal
        Vector3 targetPos = targetBird.transform.position;

        // Lerp hacia la posición del pájaro con un poco de retraso
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);


        if (playersee == true)
        {
            VolverABase();
        }
        if (runeMaterial == null) return;

        currentIntensity = Mathf.Lerp(
            currentIntensity,
            targetIntensity,
            Time.deltaTime * ObjectManager.instance.prebarkEmissionSpeed
        );

        ApplyEmission(currentIntensity);
    }

    void LateUpdate()
    {
        if (targetBird == null) return;

        Transform lookTarget = targetBird.GetLookTarget();
        if (lookTarget == null) return;

        Vector3 direction = lookTarget.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 euler = lookRotation.eulerAngles;
        euler.x = 0f;
        euler.z = 0f;
        euler.y -= 90f; // offset según tu modelo

        Quaternion finalRotation = Quaternion.Euler(euler);

        // Decide la velocidad: rápida al volar, lenta en A/C
        float step = targetBird.moving ? rotationSpeedFlying : rotationSpeedIdle;

        // Giro suave usando RotateTowards (grados por segundo)
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            finalRotation,
            step * Time.deltaTime
        );
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prebark") && ObjectManager.instance.barkAvailable && !isActive && targetBird.waitingForBark)
        {
            ActivarIluminacion();
            Debug.Log("entro");
        }
     
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Prebark") && !isActive && !playersee && ObjectManager.instance.barkAvailable &&  targetBird.waitingForBark)
        {
            ActivarIluminacion();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        VolverABase();
    }
    public void ResetRune()
    {
        isActive = false;
        playersee = false;
        VolverABase();
    }
    private void VolverABase()
    {
        targetIntensity = baseEmissionIntensity;

    }
    private void ApplyEmission(float intensity)
    {
        Color emission = glowColor * intensity;

        runeMaterial.SetColor("_EmissionColor", emission);
        DynamicGI.SetEmissive(pedestalRenderer, emission);

        runeMaterial2.SetColor("_EmissionColor", emission);
        DynamicGI.SetEmissive(pedestalRenderer2, emission);
    }
    private void ActivarIluminacion()
    {
        targetIntensity = baseEmissionIntensity * highlightMultiplier;
    }
    private void ApplyBaseEmission()
    {
        Color emission = glowColor * baseEmissionIntensity;

        runeMaterial.SetColor("_EmissionColor", emission);
        DynamicGI.SetEmissive(pedestalRenderer, emission);

        runeMaterial2.SetColor("_EmissionColor", emission);
        DynamicGI.SetEmissive(pedestalRenderer2, emission);
    }
}
