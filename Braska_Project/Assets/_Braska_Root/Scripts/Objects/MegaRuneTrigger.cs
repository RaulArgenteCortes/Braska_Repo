using UnityEngine;

public class MegaRuneTrigger : MonoBehaviour
{


    public GameObject vfx_runaActiva;
    public float vfxDuration = 2f;

    public Renderer runeRenderer;
    public Renderer PedestarlRedenderer;
    public Color glowColor = Color.cyan;

    public float glowDuration = 4.5f;

    private Material runeMaterial;
    private Material runeMaterial2;
    private Color targetEmission;
    private bool increasing = false;
    private bool decreasing = false;
    private Color initialEmission;
    private Color currentEmission;
    [SerializeField] float emissionIntensity = 25f;


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
        if (other.CompareTag("Bark") && ObjectManager.instance.megaRuneCanTrigger)
        {
            AudioManager.Instance.PlaySFX(4);
            ObjectManager.instance.megaRunePrepareMove();
            Vector3 vfxPosition = transform.position + new Vector3(0, 0.8f, 0);
            GameObject particlesystem = Instantiate(vfx_runaActiva, vfxPosition, transform.rotation);

            ShakeAllPlatforms();


            ActivarIluminacion();

            Invoke(nameof(VolverABase), glowDuration);
        }
        if(other.CompareTag("Prebark"))
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
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("MegaRunePlatform");
        foreach (var go in platforms)
        {
            MegaRunePlatform platform = go.GetComponent<MegaRunePlatform>();
            if (platform != null)
            {
                platform.TriggerShakeOnly(0.7f); 
            }
        }
    }
}

    #endregion


