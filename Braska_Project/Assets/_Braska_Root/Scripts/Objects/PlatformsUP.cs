using UnityEngine;

public class PlatformsUP : MonoBehaviour
{
    public SpringMove spring;
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            other.transform.SetParent(this.transform);
            spring.SetDown();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
