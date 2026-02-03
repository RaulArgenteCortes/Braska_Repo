using Unity.VisualScripting;
using UnityEngine;

public class KeyPick : MonoBehaviour
{
    [Header("Key Stats")]
    [SerializeField] bool isHolded;
    //[SerializeField] bool onMouth;
    public Vector3 homePosition;

    [Header("Render Stats")]
    private float emissionIntensity = 1;
    private float currentEmissionIntensity = 1;
    [SerializeField] Renderer keyRenderer;

    [Header("Object References")]
    [SerializeField] GameObject currentKeySlot;
    [SerializeField] GameObject keyHold;
    [SerializeField] ParticleSystem keyParticles;

    private void Start()
    {
        //originSlot = transform.parent.gameObject;
        keyHold = GameObject.Find("KeyHold");

        ObjectManager.instance.holdingKey = false;

        homePosition = transform.position;
    }

    private void FixedUpdate()
    {
        KeyTransform();

        EmissionUpdate();
    }

    private void KeyTransform()
    {
        if (isHolded)
        {
            if (ObjectManager.instance.keySlotOnSight)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    keyHold.transform.position,
                    Time.fixedDeltaTime * 5
                );

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    keyHold.transform.rotation,
                    Time.fixedDeltaTime * 360 * 2.5f
                );
            }
            else
            {
                transform.SetPositionAndRotation(
                    keyHold.transform.position,
                    keyHold.transform.rotation
                );
            }
        }
        else
        {
            if (currentKeySlot != null)
            {
                if (ObjectManager.instance.keySlotOnSight)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        currentKeySlot.transform.position,
                        Time.fixedDeltaTime * 5
                    );

                    transform.rotation = Quaternion.RotateTowards(
                        transform.rotation,
                        currentKeySlot.transform.rotation,
                        Time.fixedDeltaTime * 360 * 2.5f
                    );
                }
                else
                {
                    transform.SetPositionAndRotation(
                        currentKeySlot.transform.position,
                        currentKeySlot.transform.rotation
                    );
                }
            } 
        }
    }

    private void EmissionUpdate()
    {
        currentEmissionIntensity = Mathf.MoveTowards(
            currentEmissionIntensity,
            emissionIntensity,
            Time.fixedDeltaTime * ObjectManager.instance.prebarkEmissionSpeed
        );

        keyRenderer.material.SetColor("_EmissionColor", Color.white * currentEmissionIntensity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            if (ObjectManager.instance.keySlot != null)
            {
                homePosition = ObjectManager.instance.keySlot.transform.position;
                AudioManager.Instance.PlaySFX(12);
            }

            if (!ObjectManager.instance.holdingKey) // NOT holding key.
            {
                ObjectManager.instance.holdingKey = true;
                isHolded = true;
            }
            else if (ObjectManager.instance.holdingKey) // Holding key.
            {
                Invoke(nameof(DropKey), 0.1f);
            }   
        }

        if (other.CompareTag("Prebark"))
        {
            ObjectManager.instance.barkAvailable = false;

            emissionIntensity = 3;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Prebark"))
        {
            ObjectManager.instance.barkAvailable = true;

            emissionIntensity = 1;
        }
    }

    private void DropKey()
    {
        currentKeySlot = ObjectManager.instance.keySlot;

        if (!ObjectManager.instance.keySlotOnSight)
            keyParticles.Play();

        ObjectManager.instance.holdingKey = false;
        isHolded = false;
        AudioManager.Instance.PlaySFX(13);
    }

    private void PlayVFX()
    {
        //keyHold.transform.position

        //ObjectManager.instance.keySlot.transform.position
    }
}