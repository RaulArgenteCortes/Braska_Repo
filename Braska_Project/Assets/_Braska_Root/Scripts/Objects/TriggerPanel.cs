using UnityEngine;

public class TriggerPanel : MonoBehaviour
{
    public GameObject panelAactivar;
    public GameObject menuPausa;
    public GameObject menuoptions;

    private bool playerInside = false;

    private void Update()
    {
        
        if (playerInside)
        {
            if (menuPausa.activeSelf || menuoptions.activeSelf)
            {
                panelAactivar.SetActive(false);   
            }
            else
            {
                panelAactivar.SetActive(true);  
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (!menuPausa.activeSelf || !menuoptions.activeSelf)
                panelAactivar.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            panelAactivar.SetActive(false);
        }
    }
}
