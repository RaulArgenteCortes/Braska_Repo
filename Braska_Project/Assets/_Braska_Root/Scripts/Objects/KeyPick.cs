using UnityEngine;

public class KeyPick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            ObjectManager.instance.keyHold = !ObjectManager.instance.keyHold;
        }
    }
}