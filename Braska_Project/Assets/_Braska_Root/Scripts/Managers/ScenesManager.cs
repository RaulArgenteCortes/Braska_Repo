using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    [Header("Spawn stats")]
    public Vector3 spawnPoint;
    public float spawnView;

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
    }

    public void TeleportPlayer(string sceneToLoad, Vector3 newSpawnPoint, float newSpawnView)
    {
        spawnPoint = newSpawnPoint;
        spawnView = newSpawnView;

        SceneManager.LoadScene(sceneToLoad);
    }
}
