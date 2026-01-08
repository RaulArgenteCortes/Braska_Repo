using UnityEngine;

public class KeyPick : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] GameObject originSlot;
    [SerializeField] GameObject keyHold;

    private void Start()
    {
        originSlot = transform.parent.gameObject;
        keyHold = GameObject.Find("KeyHold");

        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            if (!ObjectManager.instance.holdingKey/* && transform.parent.gameObject == originSlot*/)
            {
                ObjectManager.instance.holdingKey = !ObjectManager.instance.holdingKey;

                transform.SetParent(keyHold.transform/*, true*/);

                transform.localPosition = Vector3.zero;
                transform.rotation = transform.parent.rotation;

                Debug.Log("PickUp");
            }
            else if (ObjectManager.instance.holdingKey/* && transform.parent.gameObject == originSlot*/)
            {
                ObjectManager.instance.holdingKey = !ObjectManager.instance.holdingKey;

                transform.SetParent(originSlot.transform/*, true*/);

                transform.localPosition = Vector3.zero;
                transform.rotation = transform.parent.rotation;

                Debug.Log("PickDown");
            }
        }
    }
}