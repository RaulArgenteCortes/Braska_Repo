using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] GameObject PausaMenu;
    [SerializeField] GameObject MusicaMenu;
    [SerializeField] GameObject HideaMenu;
    [SerializeField] bool isPaused = false;
    [SerializeField] string sceneToLoad;
    [SerializeField] string sceneToLoad1;

    private void Awake()
    {
        Time.timeScale = 1f;

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
        HideaMenu.SetActive(false);
        AudioManager.Instance.PauseSFX();
    }
    public void ResumeGame()
    {
        if (PausaMenu != null)
            PausaMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioManager.Instance.ResumeSFX();
        HideaMenu.SetActive(false);

    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(sceneToLoad);
        HideaMenu.SetActive(false);

    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        isPaused = false;
        HideaMenu.SetActive(false);
        SceneManager.LoadScene(sceneToLoad1);
    }
    public void Musica()
    {
        PausaMenu.SetActive(false);
        MusicaMenu.SetActive(true);
    }
}
