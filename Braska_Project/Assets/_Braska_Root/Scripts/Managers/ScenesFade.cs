using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesFade : MonoBehaviour
{
    [SerializeField] GameObject Player;

    public static ScenesFade Instance;
    Animator anim;
    public GameObject PrefabPlayer;
    [Header("Teleport VFX")]
    [SerializeField] GameObject teleportVFXPrefab;
    [SerializeField] float vfxDestroyTime = 3f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void PlayTeleportVFX(Vector3 position)
    {
        if (teleportVFXPrefab == null) return;

        GameObject vfx = Instantiate(teleportVFXPrefab, position, Quaternion.identity);
        Destroy(vfx, vfxDestroyTime);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        anim = FindFadeAnimator();
        Player = GameObject.FindGameObjectWithTag("Player");


    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        anim = FindFadeAnimator();
        Player = GameObject.FindGameObjectWithTag("Player");
    

        

      

    }


    Animator FindFadeAnimator()
    {
        GameObject fadeObj = GameObject.FindGameObjectWithTag("fade");
        if (fadeObj != null)
            return fadeObj.GetComponent<Animator>();
        return null;
    }
    private void DeactivatePlayer()
    {
        if (Player != null)
            Player.SetActive(false);
    }

    public void FadeOutAndLoad(string sceneName)
    {
        StartCoroutine(FadeAndSwitch(sceneName));
    }

    private System.Collections.IEnumerator FadeAndSwitch(string sceneName)
    {

        if (Player != null)
            PlayTeleportVFX(Player.transform.position);
        if (anim != null)
            anim.SetTrigger("FadeIn");

        Invoke(nameof(DeactivatePlayer), 0.4f);

        yield return new WaitForSeconds(0.98f);

        SceneManager.LoadScene(sceneName);

        
        anim = FindFadeAnimator();

        if (anim != null)
            anim.SetTrigger("FadeOut");

        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player != null)
            PlayTeleportVFX(Player.transform.position);

    }
}

