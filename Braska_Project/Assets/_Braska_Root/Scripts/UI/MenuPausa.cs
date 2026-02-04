using UnityEngine;
using UnityEngine.SceneManagement;
using static ScenesManager;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] GameObject PausaMenu;
    [SerializeField] GameObject MusicaMenu;
    [SerializeField] bool isPaused = false;
    [SerializeField] string sceneToLoad;
    [SerializeField] string sceneToLoad1;
    public bool isLoading = false;
    [SerializeField] GameObject[] pausePanels;
    private bool musicaMenuAbierto = false;
    [SerializeField] RunePlatform RunePlatform;

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
        if (musicaMenuAbierto)
        {
           
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CerrarMusica();
            }
            return; 
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PausaGame();
        }
    }
    
    public void PausaGame()
     {
        GameState.IsPaused = true;
        Cursor.visible = true;

        if (PausaMenu != null)
            PausaMenu.SetActive(true);
        MusicaMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        AudioManager.Instance.PauseSFX();
        SetPausePanels(false);

    }
    public void ResumeGame()
    {
        GameState.IsPaused = false;
        Cursor.visible = false;


        if (PausaMenu != null)
            PausaMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioManager.Instance.ResumeSFX();
        SetPausePanels(true);
    }
    public void MainMenu()
    {
        GameState.IsPaused = false;
        if (isLoading) return;
        isLoading = true;

        Time.timeScale = 1f;
        isPaused = false;
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad);

    }
    public void RestartLevel()
    {
        AudioManager.Instance.StopAllSFX();
        Cursor.visible = false;

        if (isLoading) return;
        isLoading = true;

        GameState.IsPaused = false;
        Time.timeScale = 1f;
        isPaused = false;

        RunePlatform.ResetToPointA();
       
        ObjectManager.instance.restartTriggered = true;
        ObjectManager.instance.runeOnPointA = true;
        ObjectManager.instance.megaRuneOnPointA = true;
      
        ObjectManager.instance.runeCanTrigger = false;
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad1);
        ObjectManager.instance.runeCanMove = false;

    }
    public void Musica()
    {
        PausaMenu.SetActive(false);
        MusicaMenu.SetActive(true);
        musicaMenuAbierto = true;
        
    }
    public void CerrarMusica()
    {
        MusicaMenu.SetActive(false);
        musicaMenuAbierto = false;
        PausaMenu.SetActive(true);
    }
    void SetPausePanels(bool active)
    {
        foreach (GameObject panel in pausePanels)
        {
            if (panel == null) continue;
            panel.SetActive(active);
        }
    }
}
