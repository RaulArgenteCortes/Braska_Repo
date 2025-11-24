using UnityEngine;

public class RuneTrigger : MonoBehaviour
{
    public GameObject vfx_runaActiva;
    public float vfxDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && ObjectManager.instance.runeCanTrigger)
        {
            AudioManager.Instance.PlaySFX(4);
            ObjectManager.instance.RunePrepareMove();
            GameObject vfx = Instantiate(vfx_runaActiva, transform.position, transform.rotation);
            Destroy(vfx, vfxDuration);
        }
    }
}
