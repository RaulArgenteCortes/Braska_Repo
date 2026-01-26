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

    private Material runeMaterial2;

    private Color currentEmission;

    [Header("Object references")]
    [SerializeField] RuneBird bird;

    Animator animator;
    private float baseEmissionIntensity = 4f;  // intensidad base del glow
    private float targetEmissionIntensity;     // intensidad objetivo al ladrido
    private float currentIntensity;            // intensidad actual usada para lerp
    private bool isGlowingHigh = false;

    public void Awake()
    {
    }
    private void Start()
    {
        if (PedestarlRedenderer != null)
        {
            runeMaterial2 = PedestarlRedenderer.material;
            runeMaterial2.EnableKeyword("_EMISSION");

            
            currentIntensity = baseEmissionIntensity;
            Color baseGlow = glowColor * currentIntensity;
            runeMaterial2.SetColor("_EmissionColor", baseGlow);
            DynamicGI.SetEmissive(PedestarlRedenderer, baseGlow);
        }



    }
    private void Update()
    {
        if (runeMaterial2 == null) return;

        float lerpSpeed = 3f; 
        float target = isGlowingHigh ? targetEmissionIntensity : baseEmissionIntensity;

        currentIntensity = Mathf.Lerp(currentIntensity, target, lerpSpeed * Time.deltaTime);

       
        if (isGlowingHigh && Mathf.Abs(currentIntensity - targetEmissionIntensity) < 0.01f)
        {
            isGlowingHigh = false; 
        }

        // Aplicamos al material
        Color emission = glowColor * currentIntensity;
        runeMaterial2.SetColor("_EmissionColor", emission);
        DynamicGI.SetEmissive(PedestarlRedenderer, emission);
    }


    private void OnTriggerEnter(Collider other)
    {
      
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

        ActivarIluminacion();

        Invoke(nameof(VolverABase), glowDuration);
    }
    
    

    private void ActivarIluminacion()
    {
        if (runeMaterial2 == null) return;

        // Intensidad más alta temporal
        targetEmissionIntensity = baseEmissionIntensity * ObjectManager.instance.runeHighEmission;
        isGlowingHigh = true;
    }
    private void VolverABase()
    {
        if (runeMaterial2 != null)
        {
            runeMaterial2.SetColor("_EmissionColor", currentEmission);
            DynamicGI.SetEmissive(PedestarlRedenderer, currentEmission);
        }
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
