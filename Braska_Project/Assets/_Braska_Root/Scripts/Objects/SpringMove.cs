using UnityEngine;

public class SpringMove : MonoBehaviour
{
    [Header("Spring Platform")]
    public GameObject springPlatform;
    public GameObject MeshSpring;
   

    [Header("Movement Settings")]
    public float downAmount = 1f;
    public float smoothTimeDown = 0.2f;
    public float smoothTimeUp = 0.1f;

    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 startingPoint;
    private Vector3 loweredPoint;

    private bool shouldBeDown = false;

    public Animator animator;
    

    public void Awake()
    {
        animator = MeshSpring.GetComponentInChildren<Animator>();

    }
    private void Start()
    {
        startingPoint = springPlatform.transform.position;

        loweredPoint = new Vector3(
            startingPoint.x,
            startingPoint.y - downAmount,
            startingPoint.z
        );
    }

    private void FixedUpdate()
    {
        MoveSpring();
    }

    private void MoveSpring()
    {
        Vector3 target = shouldBeDown ? loweredPoint : startingPoint;
        float smoothTime = shouldBeDown ? smoothTimeDown : smoothTimeUp;
        Vector3 yyy;

        /*springPlatform.transform.position = Vector3.SmoothDamp(
            springPlatform.transform.position,
            target,
            ref currentVelocity,
            smoothTime
        );*/

        yyy = Vector3.SmoothDamp(
            springPlatform.transform.position,
            target,
            ref currentVelocity,
            smoothTime
        );

        /*yyy = Mathf.SmoothDamp(
            yyy,
            target.y,
            ref currentVelocity.y,
            smoothTime
        );*/

        springPlatform.transform.position = new Vector3(springPlatform.transform.position.x, yyy.y, springPlatform.transform.position.z);
    }


    public void SetDown()
    {
        shouldBeDown = true;
        animator.SetBool("EstaAbajo?", false);
    }


    public void SetUp()
    {
        shouldBeDown = false;
        animator.SetBool("EstaAbajo?", true);
    }

   }







