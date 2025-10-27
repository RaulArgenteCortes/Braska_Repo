using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [Header("Teleport stats")]
    public string sceneToLoad;
    public Vector3 spawnPoint;
    public Vector3 spawnView;
    public bool isActive;
    [SerializeField] bool playerInside;

    void Start()
    {
        playerInside = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && playerInside)
        {
            TeleportPlayer();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void TeleportPlayer()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
