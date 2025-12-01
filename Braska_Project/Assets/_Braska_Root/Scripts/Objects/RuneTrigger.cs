using UnityEngine;

public class RuneTrigger : MonoBehaviour
{

    public Renderer runeRenderer;
    public Renderer PedestarlRedenderer;
    public Color glowColor = Color.cyan;

    public float glowDuration = 4.5f;

    private Material runeMaterial;
    private Material runeMaterial2;

    private Color currentEmission; 

    private void Start()
    {
        currentEmission = glowColor * ObjectManager.instance.runeLowEmission;

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

}
