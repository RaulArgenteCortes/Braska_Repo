using Unity.VisualScripting;
using UnityEngine;

public class KeyPick : MonoBehaviour
{
    [Header("a")]
    [SerializeField] bool isHolded;

    [Header("Object References")]
    [SerializeField] GameObject originSlot;
    [SerializeField] GameObject keyHold;

    private void Start()
    {
        originSlot = transform.parent.gameObject;
        keyHold = GameObject.Find("KeyHold");

        transform.localPosition = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (isHolded)
        {
            transform.position = keyHold.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            if (!ObjectManager.instance.holdingKey)
            {
                ObjectManager.instance.holdingKey = true;

                isHolded = true;

                transform.localPosition = Vector3.zero;
                transform.rotation = transform.parent.rotation;
            }
            else if (ObjectManager.instance.holdingKey)
            {
                ObjectManager.instance.holdingKey = false;

                isHolded = false;

                transform.localPosition = Vector3.zero;
                transform.rotation = transform.parent.rotation;
            }

            Debug.Log("KEYGEN");
        }
    }
}