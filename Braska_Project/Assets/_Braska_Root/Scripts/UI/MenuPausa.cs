using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] GameObject PausaMenu;
    [SerializeField] GameObject MusicaMenu;
    [SerializeField] bool isPaused = false;
    [SerializeField] string sceneToLoad;
    [SerializeField] string sceneToLoad1;
    public bool isLoading = false;

    private void Start()
    {
        Cursor.visible = false;

    }
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
        Cursor.visible = true;

        if (PausaMenu != null)
            PausaMenu.SetActive(true);
        MusicaMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        AudioManager.Instance.PauseSFX();
    }
    public void ResumeGame()
    {
        Cursor.visible = false;


        if (PausaMenu != null)
            PausaMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioManager.Instance.ResumeSFX();

    }
    public void MainMenu()
    {
        if (isLoading) return;
        isLoading = true;

        Time.timeScale = 1f;
        isPaused = false;
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad);

    }
    public void RestartLevel()
    {
        Cursor.visible = false;

        if (isLoading) return;
        isLoading = true;


        Time.timeScale = 1f;
        isPaused = false;
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad1);
    }
    public void Musica()
    {
        PausaMenu.SetActive(false);
        MusicaMenu.SetActive(true);
    }
}
