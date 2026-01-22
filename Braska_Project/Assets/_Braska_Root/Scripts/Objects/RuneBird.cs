using Unity.VisualScripting;
using UnityEngine;

public class RuneBird : MonoBehaviour
{
    [Header("a")]
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform pointC;

    [SerializeField] float speed = 2f;

    Vector3[] path;
    int currentIndex = 0;
    int direction = 1;
   public bool moving = false;
    public RuneTriggerBird currentRune;
    public bool waitingForBark = true;
    public Animator animator;
    public float delay = 3f;

    [Header("Look Targets")]
    [SerializeField] Transform lookWhileFlying;
    [SerializeField] Transform lookIdleAtA;
    [SerializeField] Transform lookIdleAtC;

  

    void Start()
    {

        path = new Vector3[] { pointA.position, pointB.position, pointC.position };
        transform.position = path[0]; // empieza en A
        currentIndex = 0;

        if (animator != null)
        {
            animator.SetBool("IsFlying", false);
            animator.SetBool("IsLanding", false);
            animator.SetBool("Idle", true);
        }
    }

    void Update()
    {
        if (!moving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            path[currentIndex],
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, path[currentIndex]) < 0.01f)
        {
            ArrivedAtPoint();
        }
    }



    void ArrivedAtPoint()
    {
        // Si llegó a A o C, se detiene y espera ladrido
        if (currentIndex == 0 || currentIndex == 2)
        {
            moving = false;
            waitingForBark = true;
            if (animator != null)
            {
                animator.SetBool("IsFlying", false);
                animator.SetBool("IsLanding", true);
                animator.SetBool("Idle", false);
            }
            

            // Actualiza currentRune si hay runa en el punto
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (var hit in hits)
            {
                RuneTriggerBird rune = hit.GetComponent<RuneTriggerBird>();
                if (rune != null)
                {
                    currentRune = rune;
                    break;
                }
            }

            return;
        }

        // Si está en B, sigue hacia siguiente punto
        if (currentIndex == 1)
        {
            currentIndex += direction;
        }
     

    }

    public Transform GetLookTarget()
    {
        if (moving)
            return lookWhileFlying;

        if (!moving && currentIndex == 0)
            return lookIdleAtA;
        if (!moving && currentIndex == 2)
            return lookIdleAtC;

        return null;
    }

    public void StartMove()
    {
        if (!waitingForBark) return;

        waitingForBark = false;
        moving = true;

        if (currentIndex == 0) direction = 1;
        else if (currentIndex == 2) direction = -1;

        if (currentIndex == 0) currentIndex = 1;
        else if (currentIndex == 2) currentIndex = 1;

        if (animator != null)
        {
            animator.SetBool("IsFlying", true);
            animator.SetBool("IsLanding", false);
            animator.SetBool("Idle", false);
        }
    }
}





