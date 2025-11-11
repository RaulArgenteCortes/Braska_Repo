using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [Header("Spawn stats")]
    [SerializeField] string sceneToLoad;
    [SerializeField] Vector3 newSpawnPoint;
    [SerializeField] float newSpawnView;

    [Header("Status stats")]
    [SerializeField] bool playerInside;
    [SerializeField] int requiredOrbs;
    public bool isActive;
    public bool isHighLighted;

    [Header("Object references")]
    [SerializeField] GameObject teleportLight;

    private void Start()
    {
        playerInside = false;

        isHighLighted = false;

        if (ScenesManager.instance.collectedOrbs < requiredOrbs)
        {
            isActive = false;
        }

        if (!isActive)
        {
            teleportLight.SetActive(false);
        }
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
