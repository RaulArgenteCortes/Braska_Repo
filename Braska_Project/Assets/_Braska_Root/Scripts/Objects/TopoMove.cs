using UnityEngine;

public class TopoMove : MonoBehaviour
{
    public float burrowDepth = 2f;
    public float moveSpeed = 4f;
    public float timeUnderground = 3f;
    public Transform player;
    public Transform lookTarget;

    [HideInInspector] public bool playerNearby = false;

    private Quaternion lookStartRotation;


    private Vector3 startPos;
    private Vector3 downPos;
    private Collider col;

    private bool goingDown = false;
    private bool goingUp = false;

    private void Start()
    {
        startPos = transform.position;
        downPos = startPos + new Vector3(0, -1f, 0);
        col = GetComponent<Collider>();
        lookStartRotation = lookTarget.localRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark"))
        {
            if (goingUp) return;   
            Burrow();
        }
    }
    private void Update()
    {
        FindPlayer();

        if (player == null || lookTarget == null) return;

        if (goingDown || goingUp)
        {
            lookTarget.localRotation = lookStartRotation;
            return;
        }

        LookPlayer();
    }
    private void LookPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRot = Quaternion.LookRotation(direction);
        lookTarget.localRotation = Quaternion.Euler(
            0f,
            targetRot.eulerAngles.y,
            0f
        );
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
    private void FindPlayer()
    {
        if (player != null) return;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
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