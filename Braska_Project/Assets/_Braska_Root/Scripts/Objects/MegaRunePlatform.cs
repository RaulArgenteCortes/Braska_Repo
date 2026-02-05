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
    public bool lockInA = true;
    public bool megaRunemove = false;

    private void Start()
    {
        lockInA = true;
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
        if (lockInA)
        {
            transform.position = point_A.transform.position;
            ObjectManager.instance.megaRuneOnPointA = true;

            return;
        }

    }


   public void MovePlatform()
    {
        if (lockInA)
            return;
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

    public void MegaResetToPointA()
    {
        lockInA = true;


        CancelInvoke();
        isShaking = false;
        glowing = false;
        goingToB = false;
        megaRunemove = false;

        ObjectManager.instance.megaRuneOnPointA = true;
        ObjectManager.instance.megaRuneCanMove = false;


        transform.position = point_A.transform.position;
        mesh.transform.localPosition = Vector3.zero;

        Invoke(nameof(FinishReset), 0.02f);
    }
    private void FinishReset()
    {
        ObjectManager.instance.megaRuneOnPointA = true;
        ObjectManager.instance.megaRuneCanTrigger = true;
    }

    public void ActivarGlow()
    {
        AudioManager.Instance.PlaySFX(14);

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

    public void UnlockmegaRune()
    {
        lockInA = false;
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


