using UnityEngine;

public class PlatformsOut : MonoBehaviour
{
    public SpringMove spring;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);

            spring.SetUp();
            AudioManager.Instance.PlaySFX(21);

        }
    }
}
