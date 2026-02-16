using System;
using Unity.VisualScripting;
using UnityEngine;

public class RuneTriggerBird : MonoBehaviour
{
    public GameObject vfx_runaActiva;
    public float vfxDuration = 2f;

    public Renderer runeRenderer;
    public Renderer pedestalRenderer;
    public Color glowColor = Color.cyan;

    public float glowDuration = 4.5f;
    [Header("Object references")]
    [SerializeField] RuneBird bird;

    private Material runeMaterial;
    [Header("Emission Settings")]
    [SerializeField] float baseEmissionIntensity = 4f;
    [SerializeField] float highlightMultiplier = 1.5f;

    public float currentIntensity;
    public float targetIntensity;
    private bool isActive = false;
    public bool playersee = false;
    public float timemove = 4.5f;




    private void Start()
    {
        if (pedestalRenderer == null) return;

        // Instancia única del material
        runeMaterial = pedestalRenderer.material;
        runeMaterial.EnableKeyword("_EMISSION");

        ApplyBaseEmission();

        currentIntensity = baseEmissionIntensity;
        targetIntensity = baseEmissionIntensity;
    }




    private void Update()
    {
        if (playersee == true)
        {
            VolverABase();
        }
        if (runeMaterial == null) return;

        currentIntensity = Mathf.Lerp(
            currentIntensity,
            targetIntensity,
            Time.deltaTime * ObjectManager.instance.prebarkEmissionSpeed
        );

        ApplyEmission(currentIntensity);



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prebark") && bird.currentRune == this && ObjectManager.instance.barkAvailable && !isActive && bird.waitingForBark)
        {
            ActivarIluminacion();
        }
        if (!other.CompareTag("Bark")) return;

        if (!ObjectManager.instance.barkAvailable) return;
        if (!ObjectManager.instance.runeCanTrigger) return;
        UnlockAllPlatforms();

        isActive = true;
        playersee = true;
        if (bird.currentRune != this || !bird.waitingForBark) return;
        bird.StartMove();
        ObjectManager.instance.RunePrepareMove();
        Invoke(nameof(ResetRune), timemove);

       

        ShakeAllPlatforms();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Prebark") && !isActive && !playersee && ObjectManager.instance.barkAvailable && bird.currentRune == this && bird.waitingForBark)
        {
            ActivarIluminacion();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        VolverABase();

    }
    private void UnlockAllPlatforms()
    {


        GameObject[] platforms = GameObject.FindGameObjectsWithTag("RunePlatform");

        foreach (var go in platforms)
        {
            RunePlatform platform = go.GetComponent<RunePlatform>();
            if (platform != null)
            {
                platform.UnlockRune();
            }
        }
    }
    private void ResetRune()
    {
        isActive = false;
        playersee = false;
        VolverABase();
    }
    private void ApplyBaseEmission()
    {
        Color emission = glowColor * baseEmissionIntensity;
        runeMaterial.SetColor("_EmissionColor", emission);

        // Solo una vez
        DynamicGI.SetEmissive(pedestalRenderer, emission);
    }

    private void ActivarIluminacion()
    {
       targetIntensity = baseEmissionIntensity * highlightMultiplier;
    }
    private void VolverABase()
    {
        targetIntensity = baseEmissionIntensity;
    }
    private void ApplyEmission(float intensity)
    {
        Color emission = glowColor * intensity;
        runeMaterial.SetColor("_EmissionColor", emission);
        DynamicGI.SetEmissive(pedestalRenderer, emission);
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
