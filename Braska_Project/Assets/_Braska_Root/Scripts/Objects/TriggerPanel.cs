using UnityEngine;

public class TriggerPanel : MonoBehaviour
{
    public GameObject panelAactivar;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
    

            panelAactivar.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panelAactivar.SetActive(false);
        }
    }
}
