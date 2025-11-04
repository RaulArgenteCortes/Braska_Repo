using UnityEngine;

public class RuneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && ObjectManager.instance.runeCanTrigger)
        {
            ObjectManager.instance.RunePrepareMove();
        }
    }
}
