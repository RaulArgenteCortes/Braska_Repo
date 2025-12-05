using UnityEngine;

public class TopoTrigger : MonoBehaviour
{
    public TopoMove topo; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            topo.playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            topo.playerNearby = false;
        }
    }
}

