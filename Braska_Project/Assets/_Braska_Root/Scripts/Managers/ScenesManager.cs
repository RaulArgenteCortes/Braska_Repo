using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    [Header("Spawn stats")]
    public Vector3 spawnPoint;
    public float spawnView;

    [Header("Progress stats")]
    public int collectedOrbs;

    private void Awake()
    {
        // Makes sure that there's always 1 instance.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        collectedOrbs = -1;

        ProgressCorrector();
    }

    public void TeleportPlayer(string sceneToLoad, Vector3 newSpawnPoint, float newSpawnView)
    {
        spawnPoint = newSpawnPoint;
        spawnView = newSpawnView;

        ObjectManager.instance.runeOnPointA = true; // Makes sure that the runes are on place.
        ObjectManager.instance.geyserIsUp = false; // Makes sure that the geysers are on place.

        if (ObjectManager.instance.hasOrb)
        {
            collectedOrbs += 1;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ProgressCorrector()
    {
        if ((SceneManager.GetActiveScene().name == "SCN_Level1" || SceneManager.GetActiveScene().name == "SCN_Lobby") && collectedOrbs < 0)
        {
            collectedOrbs = 0;
        }
        else if (SceneManager.GetActiveScene().name == "SCN_Level2" && collectedOrbs < 1)
        {
            collectedOrbs = 1;
        }
        else if (SceneManager.GetActiveScene().name == "SCN_Level3" && collectedOrbs < 2)
        {
            collectedOrbs = 2;
        }
        else if (SceneManager.GetActiveScene().name == "SCN_Level4" && collectedOrbs < 3)
        {
            collectedOrbs = 3;
        }
        else if (SceneManager.GetActiveScene().name == "SCN_Finale" && collectedOrbs < 4)
        {
            collectedOrbs = 4;
        }
    }
}
