using UnityEngine;

public class MegaRunePlatform : MonoBehaviour
{
    [Header("Move stats")]
    public float distance;

    [Header("Object references")]
    public GameObject point_A;
    public GameObject point_B;

    [Header("Glow settings")] 
    public Renderer platformRenderer; 
    public Color glowColor = Color.red;
    public float glowDuration = 3f;
    private Material platformMaterial;
    private bool glowing = false;
    private bool goingToB = false;
    public Color baseEmissionColor = Color.red;

    [Header("Shake settings")]
    public GameObject mesh;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;
    public float DelayAfterShake = 0.1f;

    private bool isShaking = false;
    private float shakeElapsed = 0f;
    private Vector3 originalPos;
    

    private void Start()
    {
        transform.position = point_A.transform.position;
        distance = Vector3.Distance(point_A.transform.position, point_B.transform.position);


        if (platformRenderer != null)
        {
            platformMaterial = platformRenderer.material; 

            platformMaterial.EnableKeyword("_EMISSION");
            platformMaterial.SetColor("_EmissionColor", baseEmissionColor * ObjectManager.instance.megaRuneLowEmission);
            DynamicGI.SetEmissive(platformRenderer, baseEmissionColor * ObjectManager.instance.megaRuneLowEmission);
        }
    }

    private void FixedUpdate()
    {
        
        MovePlatform();
    }
    private void Update()
    {
     
        if (isShaking)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float zOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            mesh.transform.position = originalPos + new Vector3(xOffset, 0, zOffset);

            shakeElapsed += Time.deltaTime;
            if (shakeElapsed >= shakeDuration)
            {
                mesh.transform.position = originalPos;
                isShaking = false;
            }
        }

      
    }


   public void MovePlatform()
    {
        if (ObjectManager.instance.megaRuneOnPointA && ObjectManager.instance.megaRuneCanMove)
        {
            if (!goingToB)
            {
                goingToB = true;
                ActivarGlow();
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                point_B.transform.position,
                ObjectManager.instance.megaRuneMoveTime * Time.deltaTime * distance
            );
        }
        else if (ObjectManager.instance.megaRuneCanMove)
        {
            if (goingToB)
            {
                goingToB = false;
                ActivarGlow();
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                point_A.transform.position,
                ObjectManager.instance.megaRuneMoveTime * Time.deltaTime * distance);
        }

    }



    public void ActivarGlow()
    {
        if (!glowing && platformMaterial != null)
        {
            glowing = true;

            platformMaterial.SetColor("_EmissionColor", glowColor * ObjectManager.instance.megaRuneHighEmission);
            DynamicGI.SetEmissive(platformRenderer, glowColor * ObjectManager.instance.megaRuneHighEmission);

            CancelInvoke(nameof(VolverAEmisionBase));
            Invoke(nameof(VolverAEmisionBase), glowDuration);
        }
    }
    public void VolverAEmisionBase()
    {
        glowing = false;
        platformMaterial.SetColor("_EmissionColor", baseEmissionColor * ObjectManager.instance.megaRuneLowEmission);
        DynamicGI.SetEmissive(platformRenderer, baseEmissionColor * ObjectManager.instance.megaRuneLowEmission);
    }



    public void TriggerShakeOnly(float delay)
    {
        Invoke(nameof(StartShake), delay);
    }

    private void StartShake()
    {
        originalPos = mesh.transform.position;
        shakeElapsed = 0f;
        isShaking = true;
    }


}


