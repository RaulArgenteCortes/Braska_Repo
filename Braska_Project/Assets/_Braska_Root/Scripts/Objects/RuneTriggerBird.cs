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

    private Material runeMaterial;
    private Material runeMaterial2;

    private Color currentEmission;

    [Header("Object references")]
    [SerializeField] GameObject birdPoint;
    [SerializeField] GameObject bird;

    private void Start()
    {
        /*currentEmission = glowColor * ObjectManager.instance.runeLowEmission;

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
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && ObjectManager.instance.runeCanTrigger && bird.transform.position == birdPoint.transform.position)
        {
            bird.GetComponent<RuneBird>().onPointA = !bird.GetComponent<RuneBird>().onPointA;

            ObjectManager.instance.RunePrepareMove();

            //Vector3 vfxPosition = transform.position + new Vector3(0, 0.4f, 0);
            //GameObject particlesystem = Instantiate(vfx_runaActiva, vfxPosition, transform.rotation);
            //AudioManager.Instance.PlaySFX(4);

            ShakeAllPlatforms();

            //ActivarIluminacion();

            //Invoke(nameof(VolverABase), glowDuration);
        }
    }

    private void ActivarIluminacion()
    {
        if (runeMaterial != null)
        {
            runeMaterial.EnableKeyword("_EMISSION");
            runeMaterial.SetColor("_EmissionColor", glowColor * ObjectManager.instance.runeHighEmission);  // Intensidad del brillo
        }
        if (runeMaterial2 != null)
        {
            runeMaterial2.EnableKeyword("_EMISSION");
            runeMaterial2.SetColor("_EmissionColor", glowColor * ObjectManager.instance.runeHighEmission);  // Intensidad del brillo
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
