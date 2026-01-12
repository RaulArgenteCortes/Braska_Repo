using Unity.VisualScripting;
using UnityEngine;

public class KeyPick : MonoBehaviour
{
    [Header("Key Stats")]
    [SerializeField] bool isHolded;
    [SerializeField] bool onMouth;
    public Vector3 homePosition;

    [Header("Object References")]
    //[SerializeField] GameObject originSlot;
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

        OnMouthChek();
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
            if (ObjectManager.instance.keySlot != null)
            {
                transform.SetPositionAndRotation(
                    homePosition,
                    ObjectManager.instance.keySlot.transform.rotation
                );
            }
        }
    }

    private void OnMouthChek()
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
                ObjectManager.instance.holdingKey = false;
                isHolded = false;

                PlayVFX();
            }   
        }
    }

    private void PlayVFX()
    {
        //keyHold.transform.position

        //ObjectManager.instance.keySlot.transform.position
    }
}