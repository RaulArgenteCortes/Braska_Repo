using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string sceneToLoad1;
    [SerializeField] string sceneToLoad2;
    public void Play()
    {
        AudioManager.Instance.PlaySFX(0);
        SceneManager.LoadScene(sceneToLoad1);
    }

    public void Quit()
    {
        AudioManager.Instance.PlaySFX(0);
        Application.Quit();
    }
    public void Options()
    {
        AudioManager.Instance.PlaySFX(0);
        SceneManager.LoadScene(sceneToLoad2);
    }
    public void Menu()
    {
        AudioManager.Instance.PlaySFX(0);
        SceneManager.LoadScene(sceneToLoad);
    }
    public void Start()
    {
        AudioManager.Instance.PlayMusic(0);
    }

}
