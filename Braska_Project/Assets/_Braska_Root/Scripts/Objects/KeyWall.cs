using UnityEngine;

public class KeyWall : MonoBehaviour
{
    [Header("Wall Stats")]
    [SerializeField] float wallOff;

    [Header("Object References")]
    [SerializeField] Collider wallCollider;

    public void ChangeWall()
    {
        if (ObjectManager.instance.keyHold)
        {
            wallCollider.transform.position = new Vector3 (0, wallOff, 0);
        }
        else
        {
            wallCollider.transform.position = new Vector3(0, 0, 0);
        }
    }
}
