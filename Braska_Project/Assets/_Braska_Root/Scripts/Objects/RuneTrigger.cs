using UnityEngine;

public class RuneTrigger : MonoBehaviour
{
    public GameObject vfx_runaActiva;
    public float vfxDuration = 2f;

    public Renderer runeRenderer;
    public Renderer PedestarlRedenderer;
    public Color glowColor = Color.cyan;

    public float glowDuration = 20f;

    private Material runeMaterial;
    private Material runeMaterial2;

    private void Start()
    {
        if (runeRenderer != null)
        {
            runeMaterial = runeRenderer.material;
            runeMaterial.DisableKeyword("_EMISSION");
        }
        if (PedestarlRedenderer != null)
        {
            runeMaterial2 = PedestarlRedenderer.material;
            runeMaterial.DisableKeyword("_EMISSION");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && ObjectManager.instance.runeCanTrigger)
        {
            AudioManager.Instance.PlaySFX(4);
            ObjectManager.instance.RunePrepareMove();
            GameObject vfx = Instantiate(vfx_runaActiva, transform.position, transform.rotation);
            Destroy(vfx, vfxDuration);

            ActivarIluminacion();

            Invoke(nameof(DesactivarIluminacion), glowDuration);
        }
    }
    
    private void ActivarIluminacion()
    {
        if (runeMaterial != null)
        {
            runeMaterial.EnableKeyword("_EMISSION");
            runeMaterial.SetColor("_EmissionColor", glowColor * 2f);  // Intensidad del brillo
        }
        if (runeMaterial2 != null)
        {
            runeMaterial2.EnableKeyword("_EMISSION");
            runeMaterial2.SetColor("_EmissionColor", glowColor * 16f);  // Intensidad del brillo
        }
    }
    private void DesactivarIluminacion()
    {
        if (runeMaterial != null)
        {
            runeMaterial.SetColor("_EmissionColor", Color.black);
            runeMaterial.DisableKeyword("_EMISSION");
        }
        if (runeMaterial2 != null)
        {
            runeMaterial2.SetColor("_EmissionColor", Color.black);
            runeMaterial2.DisableKeyword("_EMISSION");
        }
    }

}
