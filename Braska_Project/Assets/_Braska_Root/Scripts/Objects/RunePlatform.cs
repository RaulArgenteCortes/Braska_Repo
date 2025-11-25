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
    public float glowIntensity = 5f;
    public float glowDuration = 5f;

    private Material platformMaterial;

    private void Start()
    {
        transform.position = point_A.transform.position;
        distance = Vector3.Distance(point_A.transform.position, point_B.transform.position);

        if (platformRenderer != null )
        {
            platformMaterial = platformRenderer.material;
            platformMaterial.DisableKeyword("_EMISSION");
            DynamicGI.SetEmissive(platformRenderer, Color.black);
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
            transform.position = Vector3.MoveTowards(
                transform.position,
                point_B.transform.position,
                ObjectManager.instance.runeMoveTime * Time.deltaTime * distance);
            
        }
        else if (ObjectManager.instance.runeCanMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                point_A.transform.position,
                ObjectManager.instance.runeMoveTime * Time.deltaTime * distance
            );
        }
    }

    public void ActivarGlow()
    {
        if (platformMaterial != null)
        {
            platformMaterial.EnableKeyword("EMISSION");
            platformMaterial.SetColor("_EmissionColor", glowColor * glowIntensity);
            DynamicGI.SetEmissive(platformRenderer, glowColor * glowIntensity);
            Invoke(nameof(DesactivateGlow), glowDuration);
        }
    }
    public void DesactivateGlow()
    {
        if (platformMaterial != null)
        {
            platformMaterial.SetColor("_EmissionColor", Color.black);
            platformMaterial.DisableKeyword("_EMISSION");
            DynamicGI.SetEmissive(platformRenderer, Color.black);

        }
    }

}
