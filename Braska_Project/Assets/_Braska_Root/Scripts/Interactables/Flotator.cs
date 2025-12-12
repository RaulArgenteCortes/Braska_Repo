using UnityEngine;

public class Flotator : MonoBehaviour
{
    [Header("Flotatoe stats")]
    [SerializeField] float startingPoint;
    [SerializeField] float currentPosition;
    [SerializeField] float highFloat;
    [SerializeField] float lowFloat;
    [SerializeField] float floatSpeed;
    [SerializeField] bool isHigh;
    private float velocity;

    private void Start()
    {
        startingPoint = transform.position.y;
        currentPosition = transform.position.y;
    }

    private void Update()
    {
        TrackPosition();
    }

    private void FixedUpdate()
    {
        Flotate();
    }

    private void TrackPosition()
    {
        if (transform.position.y >= startingPoint + highFloat - 0.001f)
        {
            isHigh = true;
        }
        else if (transform.position.y <= startingPoint + lowFloat + 0.001f)
        {
            isHigh = false;
        }
    }

    private void Flotate()
    {
        currentPosition = Mathf.SmoothDamp(
            currentPosition,
            startingPoint + (isHigh ? lowFloat : highFloat),
            ref velocity,
            100 / floatSpeed * Time.deltaTime
        );

        transform.position = new Vector3(transform.position.x, currentPosition, transform.position.z);
    }
}
