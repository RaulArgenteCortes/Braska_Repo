using UnityEngine;

public class RuneTrigger : MonoBehaviour
{


    public GameObject vfx_runaActiva;
    public float vfxDuration = 2f;

    public Renderer runeRenderer;
    public Renderer PedestarlRedenderer;
    public Color glowColor = Color.cyan;

    public float glowDuration = 4.5f;

    private Material runeMaterial;
    private Material runeMaterial2;

    private Color currentEmission;
    [SerializeField] float emissionIntensity = 25f;
    private Color targetEmission;
    private bool increasing = false;
    private bool decreasing = false;
      private Color initialEmission;
    private bool isActive = false;
    public float timemove = 4.5f;
    public bool playersee = false;


    void UpdateEmission()
    {
        if (!increasing && !decreasing) return;

        // Lerp de la emisión actual hacia la target
        currentEmission = Color.Lerp(currentEmission, targetEmission, Time.deltaTime * ObjectManager.instance.prebarkEmissionSpeed);

        if (runeMaterial != null)
            runeMaterial.SetColor("_EmissionColor", currentEmission);

        if (runeMaterial2 != null)
            runeMaterial2.SetColor("_EmissionColor", currentEmission);

        // Si ya llegó casi al objetivo
        if (Vector4.Distance(currentEmission, targetEmission) < 0.01f)
        {
            currentEmission = targetEmission;
            increasing = false;
            decreasing = false;
            CancelInvoke(nameof(UpdateEmission));
        }
      
    }
    private void Update()
    {
        if (playersee == true)
        {
            VolverABase();
        }

    }
    private void Start()
    {
        currentEmission = glowColor * ObjectManager.instance.runeLowEmission;
        initialEmission = currentEmission;


        if (runeRenderer != null)
        {
            runeMaterial = runeRenderer.material;
            runeMaterial.EnableKeyword("_EMISSION");
            runeMaterial.SetColor("_EmissionColor", currentEmission);
            DynamicGI.SetEmissive(runeRenderer, currentEmission);
        }

        if (PedestarlRedenderer != null)
        {
            runeMaterial2 = PedestarlRedenderer.material;
            runeMaterial2.EnableKeyword("_EMISSION");
            runeMaterial2.SetColor("_EmissionColor", currentEmission);
            DynamicGI.SetEmissive(PedestarlRedenderer, currentEmission);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && ObjectManager.instance.runeCanTrigger && ObjectManager.instance.barkAvailable)
        {
            AudioManager.Instance.PlaySFX(4);
            ObjectManager.instance.RunePrepareMove();
            Vector3 vfxPosition = transform.position + new Vector3(0, 0.4f, 0);
            GameObject particlesystem = Instantiate(vfx_runaActiva, vfxPosition, transform.rotation);
            isActive = true;
            playersee = true;
            CancelInvoke(nameof(UpdateEmission));
            increasing = false;
            decreasing = false;
            Invoke(nameof(Moveplatforms), timemove);
            ShakeAllPlatforms();

        }
        if(other.CompareTag("Prebark") && !isActive)
        {
            ActivarIluminacion();
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Prebark") && !isActive && !playersee)
        {
            ActivarIluminacion();
        }
    }
    void Moveplatforms()
    {
        isActive = false;
        playersee = false;

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

      

        runeMaterial.EnableKeyword("_EMISSION");
        runeMaterial2.EnableKeyword("_EMISSION");

        CancelInvoke(nameof(UpdateEmission));
        InvokeRepeating(nameof(UpdateEmission), 0f, 0.02f);
    }
    private void VolverABase()
    {
        targetEmission = initialEmission; 
        decreasing = true;
        increasing = false;


        CancelInvoke(nameof(UpdateEmission));
        InvokeRepeating(nameof(UpdateEmission), 0f, 0.02f);
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
}

    #endregion


