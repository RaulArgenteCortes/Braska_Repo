using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField] GameObject portalVFX;
    [SerializeField] ParticleSystem portalParticles;

    void Start()
    {
        if (ScenesManager.instance.collectedOrbs >= 4)
        {
            portalVFX.SetActive(true);
        }
        else
        {
            portalVFX.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScenesManager.instance.collectedOrbs >= 4)
            {
                portalParticles.transform.position = other.transform.position;
                portalParticles.Play();

                Debug.Log("fin");
            }
        }
    }
}
