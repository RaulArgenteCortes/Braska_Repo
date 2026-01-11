using Unity.VisualScripting;
using UnityEngine;

public class KeyPick : MonoBehaviour
{
    [Header("Key Stats")]
    [SerializeField] bool isHolded;
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
        if (isHolded)
        {
            transform.SetPositionAndRotation(keyHold.transform.position, keyHold.transform.rotation);
        }
        else
        {
            if (ObjectManager.instance.keySlot != null)
            {
                homePosition = ObjectManager.instance.keySlot.transform.position;
            }

            transform.SetPositionAndRotation(
                homePosition,
                ObjectManager.instance.keySlot.transform.rotation
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            if (!ObjectManager.instance.holdingKey) // NOT holding key.
            {
                ObjectManager.instance.holdingKey = true;
                isHolded = true;
            }
            else if (ObjectManager.instance.holdingKey) // Holding key.
            {
                ObjectManager.instance.holdingKey = false;
                isHolded = false;

                //homePosition = ObjectManager.instance.keySlot.transform.position;

                //transform.position = homePosition;
            }   
        }
    }
}