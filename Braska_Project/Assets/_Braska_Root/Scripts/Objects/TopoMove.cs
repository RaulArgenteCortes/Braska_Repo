using System.Data;
using Unity.Mathematics;
using UnityEngine;

public class TopoMove : MonoBehaviour
{
    public float burrowDepth = 2f;
    public float moveSpeed = 4f;
    public float timeUnderground = 3f;
    public Transform player;
    public Transform lookTarget;
    public Quaternion defaultRotation;
    public Transform parentTransform;

    [HideInInspector] public bool playerOnTop = false;
    [HideInInspector] public bool playerOnRange = false;

    public ParticleSystem dirtParticles;

    public Collider childCollider;

    public Animator animator;

    public bool goingDown = false;
    public bool goingUp = false;

    private void Start()
    {
        defaultRotation = new quaternion(
            0,
            parentTransform.rotation.eulerAngles.y,
            0,
            1
        );

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

        lookTarget.rotation = Quaternion.Slerp(
            lookTarget.rotation,
            playerOnRange ? targetRot : Quaternion.Euler(parentTransform.eulerAngles),
            Time.deltaTime * 2
        );
    }

    private void Burrow()
    {
        AudioManager.Instance.PlaySFX(22);
        if (goingDown) return;

        goingDown = true;

        if (childCollider != null) childCollider.enabled = false;

        if (animator != null)
        {
            animator.SetBool("Burrow", true);
            animator.SetBool("Unburrow", false);
            animator.SetBool("Idle", false);
        }

        dirtParticles.Play();

        CancelInvoke(nameof(TryUnburrow));
        Invoke(nameof(TryUnburrow), timeUnderground);

    }



    private void TryUnburrow()
    {
        if (playerOnTop)
        {
            Invoke(nameof(TryUnburrow), 0.5f);
            return;
        }

        Unburrow();
    }

    private void Unburrow()
    {
        AudioManager.Instance.PlaySFX(22);

        if (goingUp) return;

        goingUp = true;

        if (childCollider != null) childCollider.enabled = true;

        if (animator != null)
        {
            animator.SetBool("Burrow", false);
            animator.SetBool("Unburrow", true);
            animator.SetBool("Idle", false);
        }

        dirtParticles.Play();

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