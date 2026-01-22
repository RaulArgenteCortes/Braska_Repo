using Unity.VisualScripting;
using UnityEngine;

public class KeyPick : MonoBehaviour
{
    [Header("Key Stats")]
    [SerializeField] bool isHolded;
    [SerializeField] bool onMouth;
    public Vector3 homePosition;

    [Header("Render Stats")]
    private float emissionIntensity = 1;
    private float currentEmissionIntensity = 1;
    [SerializeField] Renderer keyRenderer;

    [Header("Object References")]
    [SerializeField] GameObject currentKeySlot;
    [SerializeField] GameObject keyHold;

    private void Start()
    {
        //originSlot = transform.parent.gameObject;
        keyHold = GameObject.Find("KeyHold");

        homePosition = transform.position;
    }

    private void FixedUpdate()
    {
        KeyTransform();

        OnMouthCheck();

        EmissionUpdate();
    }

    private void KeyTransform()
    {
        if (isHolded)
        {
            if (!onMouth)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    keyHold.transform.position,
                    Time.fixedDeltaTime * 5
                );

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    keyHold.transform.rotation,
                    Time.fixedDeltaTime * 360 * 5
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
                transform.SetPositionAndRotation(
                    currentKeySlot.transform.position,
                    currentKeySlot.transform.rotation
                );
            }
        }

        /*if (transform.position == keyHold.transform.position)
        {
            transform.parent = keyHold.transform;
        }
        else
        {
            transform.parent = null;
        }*/
    }

    private void OnMouthCheck()
    {
        if (transform.position == keyHold.transform.position)
        {
            onMouth = true;
        }
        else
        {
            onMouth = false;
        }
    }

    private void EmissionUpdate()
    {
        currentEmissionIntensity = Mathf.MoveTowards(
            currentEmissionIntensity,
            emissionIntensity,
            Time.fixedDeltaTime * 5
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
            }

            if (!ObjectManager.instance.holdingKey) // NOT holding key.
            {
                ObjectManager.instance.holdingKey = true;
                isHolded = true;
            }
            else if (ObjectManager.instance.holdingKey) // Holding key.
            {
                currentKeySlot = ObjectManager.instance.keySlot;
                //transform.SetParent(currentKeySlot.transform);

                ObjectManager.instance.holdingKey = false;
                isHolded = false;

                PlayVFX();
            }   
        }

        if (other.CompareTag("Prebark"))
        {
            emissionIntensity = 3;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Prebark"))
        {
            emissionIntensity = 1;
        }
    }

    private void PlayVFX()
    {
        //keyHold.transform.position

        //ObjectManager.instance.keySlot.transform.position
    }
}