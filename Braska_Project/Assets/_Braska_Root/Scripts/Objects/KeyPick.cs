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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            if (!ObjectManager.instance.holdingKey) // NOT holding key.
            {
                ObjectManager.instance.holdedKey = this.gameObject;

                ObjectManager.instance.holdingKey = true;    

                transform.position = homePosition;

                isHolded = true;
            }
            else if (ObjectManager.instance.holdingKey) // Holding key
            {
                ObjectManager.instance.holdingKey = false;

                isHolded = false;

                ObjectManager.instance.returnKeyToParent();
            }

            Debug.Log("KEYGEN");
        }
    }
}