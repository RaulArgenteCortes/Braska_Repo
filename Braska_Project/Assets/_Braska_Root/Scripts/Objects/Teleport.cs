using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [Header("Teleport stats")]
    public string sceneToLoad;
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
        if (other.CompareTag("Player"))
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

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            barkArea bark = other.GetComponent<barkArea>();
            if (bark != null)
            {
                bark.OnBarkEvent += TeleportPlayer;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            barkArea ladrido = other.GetComponent<barkArea>();
            if (ladrido != null)
            {
                ladrido.OnBarkEvent -= TeleportPlayer;
            }
        }
    }*/

    private void TeleportPlayer()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
