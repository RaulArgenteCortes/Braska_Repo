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
        if (SceneManager.GetActiveScene().name == "SCN_Lobby")
        {
            RenderSettings.skybox = matDark;
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            if (ScenesManager.instance.collectedOrbs == -1 || ScenesManager.instance.collectedOrbs == 2)
            {
                RenderSettings.skybox = matDay;
            }
            else if (ScenesManager.instance.collectedOrbs == 0 || ScenesManager.instance.collectedOrbs == 1)
            {
                RenderSettings.skybox = matMidDay;
            }
            else if (ScenesManager.instance.collectedOrbs == 3)
            {
                RenderSettings.skybox = matSunset;
            }
            else if (ScenesManager.instance.collectedOrbs == 4)
            {
                RenderSettings.skybox = matNight;
            }
        } 
    }
}
