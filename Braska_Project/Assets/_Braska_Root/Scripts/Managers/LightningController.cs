using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LightningController : MonoBehaviour
{
    [Header("Material references")]
    [SerializeField] Material matDark;
    [SerializeField] Material matDay;
    [SerializeField] Material matMidDay;
    [SerializeField] Material matSunset;
    [SerializeField] Material matNight;

    [Header("Object references")]
    [SerializeField] GameObject dayLight;
    public GameObject fallingSnow;
    public GameObject clouds;

    private void Start()
    {
        ScenesManager.instance.ProgressCorrector();

        SkyboxChanger();

        SunlightChanger();

        SnowChanger();

        CloudsChanger();
    }

    private void SkyboxChanger()
    {
        if (SceneManager.GetActiveScene().name == "SCN_Lobby")
        {
            RenderSettings.skybox = matDark;
            dayLight.GetComponent<Light>().colorTemperature = 4000; // Changes the sun's warmt.
        }
        else
        {
            if (ScenesManager.instance.collectedOrbs == 3)
            {
                //RenderSettings.skybox = matSunset;
                dayLight.GetComponent<Light>().colorTemperature = 5500;
            }
            else if (ScenesManager.instance.collectedOrbs == -1 || ScenesManager.instance.collectedOrbs == 2)
            {
                //RenderSettings.skybox = matDay;
                dayLight.GetComponent<Light>().colorTemperature = 7000;
            }
            else if (ScenesManager.instance.collectedOrbs == 0 || ScenesManager.instance.collectedOrbs == 1)
            {
                //RenderSettings.skybox = matMidDay;
                dayLight.GetComponent<Light>().colorTemperature = 8500;
            }
            else if (ScenesManager.instance.collectedOrbs == 4)
            {
                //RenderSettings.skybox = matNight;
                dayLight.GetComponent<Light>().colorTemperature = 10000;
            }
        }
    }

    private void SunlightChanger()
    {
        if (SceneManager.GetActiveScene().name == "SCN_Lobby")
        {
            // Changes the sun's angle.
            dayLight.transform.eulerAngles = new Vector3(
                90,
                dayLight.transform.eulerAngles.y,
                dayLight.transform.eulerAngles.z
            );

            // Changes the sun's intensity.
            dayLight.GetComponent<Light>().intensity = 0.3f;
        }
        else
        {
            if (ScenesManager.instance.collectedOrbs < 4)
            {
                dayLight.transform.eulerAngles = new Vector3(
                    75 + 30 * ScenesManager.instance.collectedOrbs,
                    dayLight.transform.eulerAngles.y,
                    dayLight.transform.eulerAngles.z
                );

                dayLight.GetComponent<Light>().intensity = 0.85f - (ScenesManager.instance.collectedOrbs * 0.15f);
            }
            else
            {
                dayLight.transform.eulerAngles = new Vector3(
                    75 + 30 * -1,
                    dayLight.transform.eulerAngles.y,
                    dayLight.transform.eulerAngles.z
                );

                dayLight.GetComponent<Light>().intensity = 0.2f;
            }
        }
    }

    private void SnowChanger()
    {
        fallingSnow = GameObject.Find("FallingSnow");
        fallingSnow.SetActive(false);

        var snowEmission = fallingSnow.GetComponent<ParticleSystem>().emission;

        snowEmission.rateOverTime = 2 + ScenesManager.instance.collectedOrbs * 1;

        if (SceneManager.GetActiveScene().name != "SCN_Lobby")
        {
            fallingSnow.SetActive(true);
        }
    }

    private void CloudsChanger()
    {
        clouds = GameObject.Find("Clouds");
        clouds.SetActive(false);

        var cloudEmission = clouds.GetComponent<ParticleSystem>().emission;

        cloudEmission.rateOverTime = 0.1f; //0.1f + ScenesManager.instance.collectedOrbs * 0.05f;

        if (SceneManager.GetActiveScene().name != "SCN_Lobby")
        {
            clouds.SetActive(true);
        }
    }
}
