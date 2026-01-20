using Unity.VisualScripting;
using UnityEngine;

public class RuneBird : MonoBehaviour
{
    [Header("a")]
    public bool onPointA;
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetFlying(bool value)
    {
        animator.SetBool("isFlying", value);
        animator.SetBool("Idle", false);
        animator.SetBool("Islanding", false);
    }

    public void Land()
    {
        animator.SetBool("isFlying", false);
        animator.SetBool("Islanding", true);
        animator.SetBool("Idle", false);
    }

    private void Start()
    {
        onPointA = true;
        animator.SetBool("Idle", true);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            onPointA ? pointA.transform.position : pointB.transform.position,
            5 * Time.fixedDeltaTime
        );
    }
}
