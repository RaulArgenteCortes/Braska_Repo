using UnityEngine;

public class KeyWall : MonoBehaviour
{
    [Header("Wall Stats")]
    [SerializeField] bool playerOnTop;
    [SerializeField] float currentWallPosition;
    private float velocity;

    [Header("Layer Stats")]
    [SerializeField] GameObject playerCheck;
    [SerializeField] float playerCheckRadius;
    [SerializeField] LayerMask playerLayer;

    [Header("Object References")]
    [SerializeField] CapsuleCollider wallCollider;
    [SerializeField] GameObject wallMesh;

    private void Update()
    {
        CheckUpdate();
    }

    private void FixedUpdate()
    {
        MoveWall();
    }

    private void CheckUpdate()
    {
        playerOnTop = Physics.CheckSphere(playerCheck.transform.position, playerCheckRadius, playerLayer);
    }

    private void MoveWall()
    {
        currentWallPosition = Mathf.SmoothDamp(
            currentWallPosition,
            ObjectManager.instance.openedWallPosition * (ObjectManager.instance.holdingKey || playerOnTop ? 1 : 0),
            ref velocity,
            10 / ObjectManager.instance.wallSpeed * Time.deltaTime
        );
        
        wallCollider.center = new Vector3(0, currentWallPosition, 0);
        wallMesh.transform.localPosition = new Vector3(0, currentWallPosition + 0.5f, 0);
    }
}
