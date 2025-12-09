using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class KeyWall : MonoBehaviour
{
    [Header("Wall Stats")]
    [SerializeField] float currentWallPosition;
    private float velocity;

    [Header("Object References")]
    [SerializeField] CapsuleCollider wallCollider;
    [SerializeField] GameObject wallMesh;

    private void FixedUpdate()
    {
        MoveWall();
    }

    private void MoveWall()
    {
        currentWallPosition = Mathf.SmoothDamp(
            currentWallPosition,
            ObjectManager.instance.openedWallPosition * (ObjectManager.instance.keyHold ? 1 : 0),
            ref velocity,
            10 / ObjectManager.instance.wallSpeed * Time.deltaTime
        );

        wallCollider.center = new Vector3(0, currentWallPosition, 0);
        wallMesh.transform.localPosition = new Vector3(0, currentWallPosition + 0.5f, 0);
    }
}
