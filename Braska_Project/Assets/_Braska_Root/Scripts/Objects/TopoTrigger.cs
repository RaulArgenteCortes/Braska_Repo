using UnityEngine;

public class TopoTrigger : MonoBehaviour
{
    public TopoMove topo; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            topo.playerOnTop = true;
        }

        if (other.CompareTag("Prebark"))
        {
            topo.playerOnRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            topo.playerOnTop = false;
        }

        if (other.CompareTag("Prebark"))
        {
            topo.playerOnRange = false;
        }
    }
}

