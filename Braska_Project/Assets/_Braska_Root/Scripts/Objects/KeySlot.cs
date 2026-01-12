using UnityEngine;

public class KeySlot : MonoBehaviour
{
    [Header("KeySlot Stats")]
    public bool hasKey;

    //[Header("Object References")]
    //[SerializeField] Transform keySon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            ObjectManager.instance.keySlot = this.gameObject;
        }
    }
}
