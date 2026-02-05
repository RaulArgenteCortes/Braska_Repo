using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField] GameObject portalVFX;
    [SerializeField] ParticleSystem portalBurstParticles;
    [SerializeField] ParticleSystem portalParticles;

    void Start()
    {
        if (ScenesManager.instance.collectedOrbs >= 4)
        {
            portalVFX.transform.localScale *= 1f;

            portalParticles.Play();
        }
        else
        {
            portalVFX.transform.localScale *= 0.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScenesManager.instance.collectedOrbs >= 4)
            {
                portalBurstParticles.transform.position = other.transform.position;
                portalBurstParticles.Play();

                //Invoke("LoadEnding", 2);
                LoadEnding();
            }
        }
    }

    private void LoadEnding()
    {
        ScenesFade.Instance.FadeOutAndLoad("SCN_Finale");
    }
}
