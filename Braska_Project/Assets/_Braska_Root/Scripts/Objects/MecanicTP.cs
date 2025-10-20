using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MecanicTP : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] bool playerinside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerinside = true;

            Ladrido ladrido = other.GetComponent<Ladrido>();
            if (ladrido != null)
            {
                ladrido.OnBarkEvent += TeleportPlayer;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerinside = false;

            Ladrido ladrido = other.GetComponent<Ladrido>();
            if (ladrido != null)
            {
                ladrido.OnBarkEvent -= TeleportPlayer;
            }
        }
    }

    private void TeleportPlayer()
    {
        if (playerinside)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
