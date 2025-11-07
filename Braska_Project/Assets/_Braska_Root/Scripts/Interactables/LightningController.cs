using UnityEditor.SearchService;
using UnityEngine;
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

    private void Start()
    {
        ScenesManager.instance.ProgressCorrector();

        SkyboxChanger();

        SunLightChanger();
    }

    private void SkyboxChanger()
    {
        if (SceneManager.GetActiveScene().name == "SCN_Lobby")
        {
            RenderSettings.skybox = matDark;
            dayLight.GetComponent<Light>().colorTemperature = 4500; // Changes the sun's warmt.
        }
        else
        {
            if (ScenesManager.instance.collectedOrbs == 3)
            {
                RenderSettings.skybox = matSunset;
                dayLight.GetComponent<Light>().colorTemperature = 6000;
            }
            else if (ScenesManager.instance.collectedOrbs == -1 || ScenesManager.instance.collectedOrbs == 2)
            {
                RenderSettings.skybox = matDay;
                dayLight.GetComponent<Light>().colorTemperature = 7500;
            }
            else if (ScenesManager.instance.collectedOrbs == 0 || ScenesManager.instance.collectedOrbs == 1)
            {
                RenderSettings.skybox = matMidDay;
                dayLight.GetComponent<Light>().colorTemperature = 9000;
            }
            else if (ScenesManager.instance.collectedOrbs == 4)
            {
                RenderSettings.skybox = matNight;
                dayLight.GetComponent<Light>().colorTemperature = 10500;
            }
        }
    }

    private void SunLightChanger()
    {
        if (SceneManager.GetActiveScene().name == "SCN_Lobby")
        {
            dayLight.transform.eulerAngles = new Vector3(
                90,
                dayLight.transform.eulerAngles.y,
                dayLight.transform.eulerAngles.z
            ); // Changes the sun's angle.

            dayLight.GetComponent<Light>().intensity = 0.1f; // Changes the sun's intensity.
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

                dayLight.GetComponent<Light>().intensity = 1f;
            }
            else
            {
                dayLight.transform.eulerAngles = new Vector3(
                    75 + 30 * -1,
                    dayLight.transform.eulerAngles.y,
                    dayLight.transform.eulerAngles.z
                );

                dayLight.GetComponent<Light>().intensity = 0.1f;
            } 
        }
    }
}
