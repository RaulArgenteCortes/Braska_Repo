using UnityEngine;

public class ColorOrbe : MonoBehaviour
{
    [Header("Level")]
    public int currentLevel;

    [Header("Renderer")]
    [SerializeField] private Renderer orbRenderer;
    [SerializeField] private Renderer orbRenderer2;

    private Material orbMaterial;
    private Material orbMaterial2;

    public Color orbColorLvl1;
    public Color orbColorLvl2;
    public Color orbColorLvl3;
    public Color orbColorLvl4;
    public Color orbColorInactive;

    [Header("Emission Settings")]
    [SerializeField] private float emissionIntensity = 2f;

    private void Awake()
    {
        if (orbRenderer == null)
            orbRenderer = GetComponent<Renderer>();

        orbMaterial = orbRenderer.material;
        orbMaterial2 = orbRenderer2.material;
        
    }
    private void Start()
    {
        SetEmissionByLevel();

    }

    private void SetEmissionByLevel()
    {
        Color emissionColor = Color.black;

        switch (currentLevel)
        {
            case 1:
                emissionColor = orbColorLvl1;
                break;
            case 2:
                emissionColor = orbColorLvl2;
                break;
            case 3:
                emissionColor = orbColorLvl3;
                break;
            case 4:
                emissionColor = orbColorLvl4;
                break;
        }

        if (ScenesManager.instance.collectedOrbs < currentLevel)
        {
            emissionColor = orbColorInactive;
        }

        orbMaterial.EnableKeyword("_EMISSION");
        orbMaterial2.EnableKeyword("_EMISSION");
        orbMaterial.SetColor("_EmissionColor", emissionColor * emissionIntensity);
        orbMaterial2.SetColor("_EmissionColor", emissionColor * emissionIntensity);
    }
}

