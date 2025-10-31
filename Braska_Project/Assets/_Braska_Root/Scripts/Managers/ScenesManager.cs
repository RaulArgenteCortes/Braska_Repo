using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    [Header("Spawn stats")]
    public Vector3 spawnPoint;
    public float spawnView;

    [Header("Object references")]
    public GameObject player;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        instance = this;
    }

    public void TeleportPlayer(string sceneToLoad, Vector3 newSpawnPoint, float newSpawnView)
    {
        SceneManager.LoadScene(sceneToLoad);

        spawnPoint = newSpawnPoint;
        spawnView = newSpawnView;

        /*player = GameObject.Find("PF_Player");

        player.transform.position = newSpawnPoint;

        player.transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            newSpawnView,
            transform.eulerAngles.z
        );*/
    }
}
