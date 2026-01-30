using Unity.VisualScripting;
using UnityEngine;

public class RuneTriggerBird : MonoBehaviour
{
    public GameObject vfx_runaActiva;
    public float vfxDuration = 2f;

    public Renderer runeRenderer;
    public Renderer pedestalRenderer;
    public Color glowColor = Color.cyan;

    public float glowDuration = 4.5f;

    private Material runeMaterial2;

    private Color currentEmission;

    [Header("Object references")]
    [SerializeField] RuneBird bird;

    private Material runeMaterial;
    [Header("Emission Settings")]
    [SerializeField] float baseEmissionIntensity = 4f;
    [SerializeField] float highlightMultiplier = 1.5f;

    public float currentIntensity;
    public float targetIntensity;




    public void Awake()
    {
    }
    private void Start()
    {
        if (pedestalRenderer == null) return;

        // Instancia única del material
        runeMaterial = pedestalRenderer.material;
        runeMaterial.EnableKeyword("_EMISSION");

        ApplyBaseEmission();

        currentIntensity = baseEmissionIntensity;
        targetIntensity = baseEmissionIntensity;
    }




    private void Update()
    {
        if (runeMaterial == null) return;

        currentIntensity = Mathf.Lerp(
            currentIntensity,
            targetIntensity,
            Time.deltaTime * ObjectManager.instance.prebarkEmissionSpeed
        );

        ApplyEmission(currentIntensity);



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prebark") && bird.currentRune == this)
        {
            ActivarIluminacion();
        }
        if (!other.CompareTag("Bark")) return;
        if (!ObjectManager.instance.barkAvailable) return;
        if (!ObjectManager.instance.runeCanTrigger) return;

        if (bird.currentRune != this || !bird.waitingForBark) return;

        bird.StartMove();
        ObjectManager.instance.RunePrepareMove();

        //Vector3 vfxPosition = transform.position + new Vector3(0, 0.4f, 0);
        //GameObject particlesystem = Instantiate(vfx_runaActiva, vfxPosition, transform.rotation);
        //AudioManager.Instance.PlaySFX(4);

        ShakeAllPlatforms();

       

    }
    private void OnTriggerExit(Collider other)
    {
        VolverABase();

    }
    private void ApplyBaseEmission()
    {
        Color emission = glowColor * baseEmissionIntensity;
        runeMaterial.SetColor("_EmissionColor", emission);

        // Solo una vez
        DynamicGI.SetEmissive(pedestalRenderer, emission);
    }

    private void ActivarIluminacion()
    {
       targetIntensity = baseEmissionIntensity * highlightMultiplier;
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
