using UnityEngine;

public class RunePlatform : MonoBehaviour
{
    [Header("Move stats")]
    public float distance;

    [Header("Object references")]
    public GameObject point_A;
    public GameObject point_B;

    [Header("Glow settings")] 
    public Renderer platformRenderer; 
    public Color glowColor = Color.cyan; 
    public float glowIntensity = 2f;
    public float glowDuration = 3f;
    private Material platformMaterial;
    private bool glowing = false;
    private bool goingToB = false;
    public Color baseEmissionColor = Color.cyan;
    public float baseEmissionIntensity = 1f;

    private void Start()
    {


        transform.position = point_A.transform.position;
        distance = Vector3.Distance(point_A.transform.position, point_B.transform.position);

        if (platformRenderer != null)
        {
            platformMaterial = platformRenderer.material; 

            platformMaterial.EnableKeyword("_EMISSION");
            platformMaterial.SetColor("_EmissionColor", baseEmissionColor * baseEmissionIntensity);
            DynamicGI.SetEmissive(platformRenderer, baseEmissionColor * baseEmissionIntensity);
        }
    }

    private void FixedUpdate()
    {
        MovePlatform();
    }

    public void MovePlatform()
    {


        if (ObjectManager.instance.runeOnPointA && ObjectManager.instance.runeCanMove)
        {
            if (!goingToB)
            {
                goingToB = true;
                ActivarGlow();
            }
            

            transform.position = Vector3.MoveTowards(
                transform.position,
                point_B.transform.position,
                ObjectManager.instance.runeMoveTime * Time.deltaTime * distance);
        }
        else if (ObjectManager.instance.runeCanMove)
        {
            if (goingToB)
            {
                goingToB = false;
                ActivarGlow();
            }
         
            transform.position = Vector3.MoveTowards(
                transform.position,
                point_A.transform.position,
                ObjectManager.instance.runeMoveTime * Time.deltaTime * distance);
        }

    }

    public void ActivarGlow()
    {
        if (!glowing && platformMaterial != null)
        {
            glowing = true;

            platformMaterial.SetColor("_EmissionColor", glowColor * glowIntensity);
            DynamicGI.SetEmissive(platformRenderer, glowColor * glowIntensity);

            CancelInvoke(nameof(VolverAEmisionBase));
            Invoke(nameof(VolverAEmisionBase), glowDuration);
        }
    }
    public void VolverAEmisionBase()
    {
        glowing = false;

        platformMaterial.SetColor("_EmissionColor", baseEmissionColor * baseEmissionIntensity);
        DynamicGI.SetEmissive(platformRenderer, baseEmissionColor * baseEmissionIntensity);
    }
}


