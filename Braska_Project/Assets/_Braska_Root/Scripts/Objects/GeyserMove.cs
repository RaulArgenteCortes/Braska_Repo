using Unity.VisualScripting;
using UnityEngine;

public class GeyserMove : MonoBehaviour
{
    [Header("Geyser stats")]
    public Vector3 startingPoint;
    public float maxHeight;
    private Vector3 currentSpeed = Vector3.zero; // Is just the current speed, it updates automarically.

    [Header("Object references")]
    public GameObject geyserPlatform;
    public GameObject water;

    private void Start()
    {
        startingPoint = geyserPlatform.transform.position;
    }

    private void Update()
    {
        WarpWater();
    }

    private void WarpWater()
    {
        water.transform.localPosition = geyserPlatform.transform.localPosition / 2;

        water.transform.localScale = new Vector3(
            water.transform.localScale.x,
            geyserPlatform.transform.localPosition.y / 2,
            water.transform.localScale.z
        );
    }

    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (ObjectManager.instance.geyserIsUp)
        {
            geyserPlatform.transform.position = Vector3.SmoothDamp( // SmoothDamp adds acceleration and deacceleration to the movement.
                geyserPlatform.transform.position,
                new Vector3(
                    geyserPlatform.transform.position.x,
                    startingPoint.y + maxHeight,
                    geyserPlatform.transform.position.z
                ),
                ref currentSpeed,
                ObjectManager.instance.geyserMoveTime * 10 * Time.deltaTime
            );
        }
        else
        {
            geyserPlatform.transform.position = Vector3.SmoothDamp(
                geyserPlatform.transform.position,
                startingPoint,
                ref currentSpeed,
                ObjectManager.instance.geyserMoveTime * 10 * Time.deltaTime
            );
        }
    }
}
