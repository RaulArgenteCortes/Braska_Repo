using UnityEngine;

public class RunePlatform : MonoBehaviour
{
    [Header("Move stats")]
    public float distance;

    [Header("Object references")]
    public GameObject point_A;
    public GameObject point_B;

    private void Start()
    {
        transform.position = point_A.transform.position;
        distance = Vector3.Distance(point_A.transform.position, point_B.transform.position);
    }

    private void FixedUpdate()
    {
        MovePlatform();
    }

    public void MovePlatform()
    {
        if (ObjectManager.instance.runeOnPointA && ObjectManager.instance.runeCanMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                point_B.transform.position,
                ObjectManager.instance.runeMoveTime * Time.deltaTime * distance
            );
        }
        else if (ObjectManager.instance.runeCanMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                point_A.transform.position,
                ObjectManager.instance.runeMoveTime * Time.deltaTime * distance
            );
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
