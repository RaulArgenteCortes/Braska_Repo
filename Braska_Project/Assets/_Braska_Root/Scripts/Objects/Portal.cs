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
                Debug.Log("fin");
            }
        }
        if (other.CompareTag("Orb"))
        {
            if (ScenesManager.instance.teleportedOrbs < 4)
            {
                portalParticles.transform.position = other.transform.position;

                other.gameObject.SetActive(false);
                ScenesManager.instance.teleportedOrbs += 1;

                portalParticles.Play();
            }
        }
    }
}
