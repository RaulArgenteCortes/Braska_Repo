using UnityEngine;

public class KeySlot : MonoBehaviour
{
    [Header("KeySlot Stats")]
    public bool hasKey;

    [Header("Render Stats")]
    private float emissionIntensity = 1;
    private float currentEmissionIntensity = 1;
    [SerializeField] Renderer slotRenderer;

    private void FixedUpdate()
    {
        currentEmissionIntensity = Mathf.MoveTowards(
            currentEmissionIntensity,
            emissionIntensity,
            Time.fixedDeltaTime * ObjectManager.instance.prebarkEmissionSpeed
        );

        slotRenderer.material.SetColor("_EmissionColor", Color.white * currentEmissionIntensity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            ObjectManager.instance.keySlot = this.gameObject;
        }

        if (other.CompareTag("Prebark"))
        {
            ObjectManager.instance.keySlotOnSight = true;

            if (!ObjectManager.instance.barkAvailable)
            {
                emissionIntensity = 3;
            }  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Prebark"))
        {
            ObjectManager.instance.keySlotOnSight = false;

            emissionIntensity = 1;
        }
    }
}
