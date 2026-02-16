using UnityEngine;

public class Flotator : MonoBehaviour
{
    [Header("Flotatoe stats")]
    [SerializeField] float highFloat;
    [SerializeField] float lowFloat;
    [SerializeField] float floatSpeed;
    private float startingPoint;
    private float currentPosition;
    private bool isHigh;
    private float velocity;

    private void Start()
    {
        startingPoint = transform.localPosition.y;
        currentPosition = transform.localPosition.y;
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
        if (transform.localPosition.y >= startingPoint + highFloat - 0.001f)
        {
            isHigh = true;
        }
        else if (transform.localPosition.y <= startingPoint + lowFloat + 0.001f)
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

        transform.localPosition = new Vector3(transform.localPosition.x, currentPosition, transform.localPosition.z);
    }
}
