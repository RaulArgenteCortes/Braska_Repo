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
            if (1 == 1)
            {
                ObjectManager.instance.keySlot = this.gameObject;

                Debug.Log("New Home");
            }
        }
    }
}
