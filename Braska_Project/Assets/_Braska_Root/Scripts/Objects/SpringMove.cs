using UnityEngine;

public class SpringMove : MonoBehaviour
{
    [Header("Spring Platform")]
    public GameObject springPlatform;     

    [Header("Movement Settings")]
    public float downAmount = 1f;
    public float smoothTimeDown = 0.2f;
    public float smoothTimeUp = 0.1f;

    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 startingPoint;
    private Vector3 loweredPoint;

    private bool shouldBeDown = false;

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
        float smoothTime = shouldBeDown ? smoothTimeDown : smoothTimeUp; // cambia según dirección

        springPlatform.transform.position = Vector3.SmoothDamp(
            springPlatform.transform.position,
            target,
            ref currentVelocity,
            smoothTime);
    }

  
    public void SetDown()
    {
        shouldBeDown = true;
    }

   
    public void SetUp()
    {
        shouldBeDown = false;
    }
}

