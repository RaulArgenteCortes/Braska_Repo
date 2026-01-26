using UnityEngine;

public class TopoMove : MonoBehaviour
{
    public float burrowDepth = 2f;
    public float moveSpeed = 4f;
    public float timeUnderground = 3f;
    public Transform player;
    public Transform lookTarget;

    [HideInInspector] public bool playerNearby = false;


    public Collider childCollider;

    public Animator animator;

    public bool goingDown = false;
    public bool goingUp = false;

    private void Start()
    {
        if (animator != null)
            animator.SetBool("Idle", true); 

    }

    public void ChildTriggered(Collider other)
    {
        if (other.CompareTag("Bark") && ObjectManager.instance.barkAvailable)
        {
            if (goingUp) return;
            Burrow();
        }
    
    }
    private void Update()
    {
        FindPlayer();

        LookPlayer();

        if (player == null || lookTarget == null) return;

  

    }
    private void LookPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;


        Quaternion targetRot = Quaternion.LookRotation(direction);

        lookTarget.rotation = Quaternion.Euler(
            0f,                    
            targetRot.eulerAngles.y, 
            0f                       
        );
    }

    private void Burrow()
    {
        if (goingDown) return;

        goingDown = true;

        if (childCollider != null) childCollider.enabled = false;

        if (animator != null)
        {
            animator.SetBool("Burrow", true);
            animator.SetBool("Unburrow", false);
            animator.SetBool("Idle", false);
        }
        CancelInvoke(nameof(TryUnburrow));
        Invoke(nameof(TryUnburrow), timeUnderground);

    }



    private void TryUnburrow()
    {
        if (playerNearby)
        {
            Debug.Log("Intento Subir");

            Invoke(nameof(TryUnburrow), 0.5f);
            return;
        }

        Unburrow();
    }

    private void Unburrow()
    {
        if (goingUp) return;

        goingUp = true;

        if (childCollider != null) childCollider.enabled = true;

        if (animator != null)
        {
            animator.SetBool("Burrow", false);
            animator.SetBool("Unburrow", true);
            animator.SetBool("Idle", false);
        }

        float unburrowDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke(nameof(FinishUnburrow), unburrowDuration);

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
  

    public void FinishUnburrow()
    {
        goingUp = false;
        goingDown = false;

        if (animator != null)
        {
            animator.SetBool("Unburrow", false);
            animator.SetBool("Idle", true);
        }
    }

}