using UnityEngine;

public class TopoTriggerChildren : MonoBehaviour
{
    public TopoMove parentTopo; 

    private void OnTriggerEnter(Collider other)
    {
        if (parentTopo != null)
        {
            parentTopo.ChildTriggered(other);
        }
    }
}
