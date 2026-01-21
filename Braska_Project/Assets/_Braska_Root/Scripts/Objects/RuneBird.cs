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
    bool moving = false;
    public RuneTriggerBird currentRune;
    public bool waitingForBark = true;
    public Animator animator;


    void Start()
    {

        path = new Vector3[] { pointA.position, pointB.position, pointC.position };
        transform.position = path[0]; // empieza en A
        currentIndex = 0;
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
    public void StartMove()
    {
        // Solo puede moverse si está esperando un ladrido
        if (!waitingForBark) return;

        waitingForBark = false;
        moving = true;

        // Determinar dirección
        if (currentIndex == 0) direction = 1; // de A a B
        else if (currentIndex == 2) direction = -1; // de C a B

        // Si está en A o C, moverse al siguiente punto
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





