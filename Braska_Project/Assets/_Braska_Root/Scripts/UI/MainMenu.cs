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
        if (ScenesManager.instance.collectedOrbs < 0)
        {

            if (isLoading) return;
            isLoading = true;
            AudioManager.Instance.PlaySFX(0);
            ScenesFade.Instance.FadeOutAndLoad(sceneToLoad1);
            Time.timeScale = 1f;
        }
        if(ScenesManager.instance.collectedOrbs > -1)
        {
            if (isLoading) return;
            isLoading = true;
            AudioManager.Instance.PlaySFX(0);
            ScenesFade.Instance.FadeOutAndLoad(sceneToLoad);
            Time.timeScale = 1f;
        }
    
    }


    public void Quit()
    {
        if (isLoading) return;
        isLoading = true;
        AudioManager.Instance.PlaySFX(0);
        Application.Quit();
      
    }
    public void Reseta()
    {
        ScenesManager.instance.SpawnTeleport = null;
        ScenesManager.instance.collectedOrbs = -1;
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
   

    public void Replay()
    {
        if (isLoading) return;
        isLoading = true;
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad);
    }
    #endregion
}
