using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [Header("Teleport stats")]
    public string sceneToLoad;
    public Vector3 newSpawnPoint;
    public float newSpawnView;
    public bool isActive;
    [SerializeField] bool playerInside;

    [Header("Script references")]
    [SerializeField] ScenesManager ScenesManager;

    void Start()
    {
        playerInside = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && playerInside)
        {
            OrderTeleport();
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

    private void OrderTeleport()
    {
        ScenesManager.instance.TeleportPlayer(sceneToLoad, newSpawnPoint, newSpawnView);
    }
}
