using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string sceneToLoad1;
    [SerializeField] string sceneToLoad2;
    public void Play()
    {
        SceneManager.LoadScene(sceneToLoad1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Options()
    {
        SceneManager.LoadScene(sceneToLoad2);
    }
    public void Menu()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
