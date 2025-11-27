using UnityEngine;

public class RuneTrigger : MonoBehaviour
{

    public Renderer runeRenderer;
    public Renderer PedestarlRedenderer;
    public Color glowColor = Color.cyan;

    public float glowDuration = 4.5f;

    private Material runeMaterial;
    private Material runeMaterial2;

    private Color baseEmissionColor; 
    private float baseIntensity = 1f;

    private void Start()
    {
        baseEmissionColor = glowColor * baseIntensity;

        if (runeRenderer != null)
        {
            runeMaterial = runeRenderer.material;
            runeMaterial.EnableKeyword("_EMISSION");
            runeMaterial.SetColor("_EmissionColor", baseEmissionColor);
            DynamicGI.SetEmissive(runeRenderer, baseEmissionColor);
        }

        if (PedestarlRedenderer != null)
        {
            runeMaterial2 = PedestarlRedenderer.material;
            runeMaterial2.EnableKeyword("_EMISSION");
            runeMaterial2.SetColor("_EmissionColor", baseEmissionColor);
            DynamicGI.SetEmissive(PedestarlRedenderer, baseEmissionColor);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && ObjectManager.instance.runeCanTrigger)
        {
            AudioManager.Instance.PlaySFX(4);
            ObjectManager.instance.RunePrepareMove();

            ActivarIluminacion();

            Invoke(nameof(VolverABase), glowDuration);
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
            runeMaterial2.SetColor("_EmissionColor", glowColor * 2f);  // Intensidad del brillo
        }
    }
    private void VolverABase()
    {
        if (runeMaterial != null)
        {
            runeMaterial.SetColor("_EmissionColor", baseEmissionColor);
            DynamicGI.SetEmissive(runeRenderer, baseEmissionColor);
        }

        if (runeMaterial2 != null)
        {
            runeMaterial2.SetColor("_EmissionColor", baseEmissionColor);
            DynamicGI.SetEmissive(PedestarlRedenderer, baseEmissionColor);
        }
    }

}
