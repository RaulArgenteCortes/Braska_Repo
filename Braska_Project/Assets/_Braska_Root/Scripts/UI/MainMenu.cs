using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region "Variables"
    [SerializeField] string sceneToLoad;
    [SerializeField] string sceneToLoad1;
    [SerializeField] string sceneToLoad2;
    #endregion
    #region "Voids"
    public void Play()
    {
        AudioManager.Instance.PlaySFX(0);
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad1);
        Time.timeScale = 1f;
        ScenesManager.instance.spawnPoint = new Vector3 (-3, 1, 0);
    }

    public void Quit()
    {
        AudioManager.Instance.PlaySFX(0);
        Application.Quit();
    }
    public void Options()
    {
        AudioManager.Instance.PlaySFX(0);
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad2);
    }
    public void Menu()
    {
        AudioManager.Instance.PlaySFX(0);
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad);
    }
    public void Start()
    {
        AudioManager.Instance.PlayMusic(0);
    }

    public void Replay()
    {
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad);
    }
    #endregion
}
