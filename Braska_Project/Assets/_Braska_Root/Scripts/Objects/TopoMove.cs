using UnityEngine;

public class TopoMove : MonoBehaviour
{
    public float burrowDepth = 2f;
    public float moveSpeed = 4f;
    public float timeUnderground = 3f;

    [HideInInspector] public bool playerNearby = false;

    private Vector3 startPos;
    private Vector3 downPos;
    private Collider col;

    private bool goingDown = false;
    private bool goingUp = false;

    private void Start()
    {
        startPos = transform.position;
        downPos = startPos + new Vector3(0, -2f, 0);
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            if (goingUp) return;   
            Burrow();
        }
    }

    private void Burrow()
    {
        if (goingDown) return;

        goingDown = true;
        col.enabled = false;

        InvokeRepeating(nameof(MoveDown), 0f, 0.01f);
        CancelInvoke(nameof(TryUnburrow));    
        Invoke(nameof(TryUnburrow), timeUnderground);
    }

    private void MoveDown()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            downPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, downPos) < 0.05f)
        {
            CancelInvoke(nameof(MoveDown));
            goingDown = false;
        }
    }

    private void TryUnburrow()
    {
        if (playerNearby)
        {
            Invoke(nameof(TryUnburrow), 0.5f);
            return;
        }

        Unburrow();
    }

    private void Unburrow()
    {
        if (goingUp) return;

        goingUp = true;
        col.enabled = true;

        InvokeRepeating(nameof(MoveUp), 0f, 0.01f);
    }

    private void MoveUp()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            startPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, startPos) < 0.05f)
        {
            CancelInvoke(nameof(MoveUp));
            goingUp = false;
        }
    }
}