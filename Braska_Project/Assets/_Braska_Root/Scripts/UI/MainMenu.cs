using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region "Variables"
    [SerializeField] string sceneToLoad;
    [SerializeField] string sceneToLoad1;
    [SerializeField] string sceneToLoad2;

    public bool isLoading = false;
    #endregion
    #region "Voids"
   
    public void Play()
    {
        if (isLoading) return;
        isLoading = true;
        AudioManager.Instance.PlaySFX(0);
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad1);
        Time.timeScale = 1f;
        
    
    }


    public void Quit()
    {
        if (isLoading) return;
        isLoading = true;
        AudioManager.Instance.PlaySFX(0);
        Application.Quit();
      
    }
    public void Options()
    {
        if (isLoading) return;
        isLoading = true;
        AudioManager.Instance.PlaySFX(0);
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad2);
    }
    public void Menu()
    {
        if (isLoading) return;
        isLoading = true;
        AudioManager.Instance.PlaySFX(0);
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad);
    }
    public void Start()
    {
        AudioManager.Instance.PlayMusic(0);
    }

    public void Replay()
    {
        if (isLoading) return;
        isLoading = true;
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad);
    }
    #endregion
}
