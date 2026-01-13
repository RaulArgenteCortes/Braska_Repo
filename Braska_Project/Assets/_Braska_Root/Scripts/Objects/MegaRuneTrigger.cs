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

    private Color currentEmission;



    private void Start()
    {
        currentEmission = glowColor * ObjectManager.instance.megaRuneLowEmission;

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
            Vector3 vfxPosition = transform.position + new Vector3(0, 0.4f, 0);
            GameObject particlesystem = Instantiate(vfx_runaActiva, vfxPosition, transform.rotation);

            ShakeAllPlatforms();


            ActivarIluminacion();

            Invoke(nameof(VolverABase), glowDuration);
        }
    }

    private void ActivarIluminacion()
    {
        if (runeMaterial != null)
        {
            runeMaterial.EnableKeyword("_EMISSION");
            runeMaterial.SetColor("_EmissionColor", glowColor * ObjectManager.instance.megaRuneHighEmission);  // Intensidad del brillo
        }
        if (runeMaterial2 != null)
        {
            runeMaterial2.EnableKeyword("_EMISSION");
            runeMaterial2.SetColor("_EmissionColor", glowColor * ObjectManager.instance.megaRuneHighEmission);  // Intensidad del brillo
        }
    }
    private void VolverABase()
    {
        if (runeMaterial != null)
        {
            runeMaterial.SetColor("_EmissionColor", currentEmission);
            DynamicGI.SetEmissive(runeRenderer, currentEmission);
        }

        if (runeMaterial2 != null)
        {

            runeMaterial2.SetColor("_EmissionColor", currentEmission);
            DynamicGI.SetEmissive(PedestarlRedenderer, currentEmission);
        }
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


