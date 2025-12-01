using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class KeyWall : MonoBehaviour
{
    [SerializeField] float currentWallPosition;

    [Header("Object References")]
    [SerializeField] CapsuleCollider wallCollider;
    [SerializeField] GameObject wallMesh;

    private void FixedUpdate()
    {
        currentWallPosition = Mathf.MoveTowards(
            currentWallPosition,
            ObjectManager.instance.targetWallPosition * (ObjectManager.instance.keyHold ? 1 : 0),
            Time.deltaTime
        );

        wallCollider.center = new Vector3(0, currentWallPosition, 0);
        wallMesh.transform.localPosition = new Vector3(0, currentWallPosition + 0.5f, 0);
    }
}
