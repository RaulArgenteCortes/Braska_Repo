using UnityEngine;
using UnityEngine.Audio;

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
    private bool lastHoldingKeyState = false;




    private void Update()
    {
        CheckUpdate();
        if (ObjectManager.instance.holdingKey != lastHoldingKeyState)
        {
            AudioManager.Instance.PlaySFX(ObjectManager.instance.holdingKey ? 18 : 19);

            lastHoldingKeyState = ObjectManager.instance.holdingKey;
        }
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
            ObjectManager.instance.openedWallPosition * (ObjectManager.instance.holdingKey || playerOnTop ? 0.75f : -0.25f),
            ref velocity,
            10 / ObjectManager.instance.wallSpeed * Time.deltaTime
        );

        wallCollider.center = new Vector3(0, currentWallPosition, 0);
        wallMesh.transform.localPosition = new Vector3(0, currentWallPosition + 0.25f, 0);

        wallMesh.transform.eulerAngles = new Vector3(
            wallMesh.transform.eulerAngles.x,
            currentWallPosition * -270,
            wallMesh.transform.eulerAngles.z
        );
    }
}
