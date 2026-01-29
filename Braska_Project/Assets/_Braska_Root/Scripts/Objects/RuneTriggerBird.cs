using Unity.VisualScripting;
using UnityEngine;

public class RuneTriggerBird : MonoBehaviour
{
    public GameObject vfx_runaActiva;
    public float vfxDuration = 2f;

    public Renderer runeRenderer;
    public Renderer PedestarlRedenderer;
    public Color glowColor = Color.cyan;

    public float glowDuration = 4.5f;


    [Header("Object references")]
    [SerializeField] RuneBird bird;

    Animator animator;
    private Material runeMaterial;
    private Color currentEmission;
    private Color targetEmission;
    private Color baseEmission;

    private bool increasing = false;
    private bool decreasing = false;
    [SerializeField] float emissionIntensity = 25f;
    [SerializeField] float prebarkEmissionSpeed = 3f;
    [SerializeField] float baseEmissionIntensity = 10f;


    public void Awake()
    {
    }
    private void Start()
    {

        if (PedestarlRedenderer == null) return;

        runeMaterial = PedestarlRedenderer.material;
        runeMaterial.EnableKeyword("_EMISSION");

        // Emission base GLOBAL
        baseEmission = glowColor * baseEmissionIntensity;
        currentEmission = baseEmission;

        runeMaterial.SetColor("_EmissionColor", baseEmission);
        DynamicGI.SetEmissive(PedestarlRedenderer, baseEmission);



    }
    private void Update()
    {
        // Si puede ladrar → brillo alto
        if (bird.currentRune == this && bird.waitingForBark)
        {
            targetEmission = glowColor * ObjectManager.instance.runeHighEmission * emissionIntensity;
        }
        else
        {
            // Si no, SIEMPRE base emission
            targetEmission = baseEmission;
        }

        currentEmission = Color.Lerp(
            currentEmission,
            targetEmission,
            Time.deltaTime * prebarkEmissionSpeed
        );

        runeMaterial.SetColor("_EmissionColor", currentEmission);
        DynamicGI.SetEmissive(PedestarlRedenderer, currentEmission);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prebark") && bird.currentRune == this && bird.waitingForBark)
        {
            ActivarIluminacion();
        }
        if (!other.CompareTag("Bark")) return;
        if (!ObjectManager.instance.barkAvailable) return;
        if (!ObjectManager.instance.runeCanTrigger) return;

        if (bird.currentRune != this || !bird.waitingForBark) return;

        bird.StartMove();
        ObjectManager.instance.RunePrepareMove();

        ShakeAllPlatforms();
   

        if (other.CompareTag("Prebark"))
        {
            ActivarIluminacion();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        VolverABase();
    }


    private void ActivarIluminacion()
    {
        targetEmission = glowColor * ObjectManager.instance.runeHighEmission * emissionIntensity;
        increasing = true;
        decreasing = false;

        CancelInvoke(nameof(Update));
        InvokeRepeating(nameof(Update), 0f, 0.02f);
    }
    private void VolverABase()
    {
        targetEmission = baseEmission; 
        decreasing = true;
        increasing = false;

        CancelInvoke(nameof(Update));
        InvokeRepeating(nameof(Update), 0f, 0.02f);
    }
    #region ShakePlatforms

    private void ShakeAllPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("RunePlatform");
        foreach (var go in platforms)
        {
            RunePlatform platform = go.GetComponent<RunePlatform>();
            if (platform != null)
            {
                platform.TriggerShakeOnly(0.7f);
            }
        }
    }


    #endregion
}
