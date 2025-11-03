using UnityEngine;

public class RunePlatform : MonoBehaviour
{
    [Header("Position stats")]
    public float moveTime;
    public float distance;
    public bool canMove;
    public bool onPoint_A;

    [Header("Object references")]
    public GameObject point_A;
    public GameObject point_B;

    private void Start()
    {
        transform.position = point_A.transform.position;
        onPoint_A = true;
        distance = Vector3.Distance(point_A.transform.position, point_B.transform.position);

        //canMove = true;
    }

    private void FixedUpdate()
    {
        MovePlatform();
    }

    public void MovePlatform()
    {
        if (onPoint_A && canMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                point_B.transform.position,
                moveTime * Time.deltaTime * distance
            );

            if (transform.position == point_B.transform.position)
            {
                onPoint_A = false;
                canMove = false;
            }
        }
        else if (canMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                point_A.transform.position,
                moveTime * Time.deltaTime * distance
            );

            if (transform.position == point_A.transform.position)
            {
                onPoint_A = true;
                canMove = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
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
