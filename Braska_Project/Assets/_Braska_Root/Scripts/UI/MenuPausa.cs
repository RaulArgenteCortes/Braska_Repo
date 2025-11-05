using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] GameObject PausaMenu;
    [SerializeField] GameObject MusicaMenu;
    [SerializeField]  bool isPaused = false;
    [SerializeField] string sceneToLoad;
    [SerializeField] string sceneToLoad1;

    private void Awake()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayMusic(1);
        isPaused = false;

        if (PausaMenu != null)
            PausaMenu.SetActive(false);
    
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PausaGame();
            }
        }
    }
    public void PausaGame()
     {
        if (PausaMenu != null)
            PausaMenu.SetActive(true);
        MusicaMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

    }
    public void ResumeGame()
    {
        if (PausaMenu != null)
            PausaMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(sceneToLoad);

    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        isPaused = false;

        SceneManager.LoadScene(sceneToLoad1);
    }
    public void Musica()
    {
        PausaMenu.SetActive(false);
        MusicaMenu.SetActive(true);
    }
}
