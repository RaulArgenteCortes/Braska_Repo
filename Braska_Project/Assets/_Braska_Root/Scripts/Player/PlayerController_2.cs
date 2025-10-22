using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.GridBrushBase;

public class CharacterController : MonoBehaviour
{
    [Header("Movement stats")]
    public float moveSpeed;
    public float moveX;
    public float moveZ;
    public Vector2 moveInput;

    [Header("Rotation stats")]
    public float playerAngle;
    public float rotationSpeed;

    [Header("References")]
    public Rigidbody playerRb;
    public GameObject playerMesh;
    public GameObject worldAxsis;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerMesh = GameObject.Find("PlayerMesh");
        worldAxsis = GameObject.Find("WorldAxsis");
    }

    private void Start()
    {
        playerRb.useGravity = true;
    }

    private void Update()
    {
        //SetMoveDirection();
    }

    private void FixedUpdate()
    {
        PlayerRotation();

        PlayerMove();
    }

    /*private void SetMoveDirection() // Sets de move direction of the player
    {
        if (moveInput.x > 0)
        {
            moveX = 1;
        }
        else if (moveInput.x < 0)
        {
            moveX = -1;
        }
        else
        {
            moveX = 0;
        }

        if (moveInput.y > 0)
        {
            moveZ = 1;
        }
        else if (moveInput.y < 0)
        {
            moveZ = -1;
        }
        else
        {
            moveZ = 0;
        }
    }*/

    private void PlayerMove()
    {
        //playerRb.transform.localPosition += new Vector3(moveX * moveSpeed, 0, moveZ * moveSpeed);
        if (moveInput != new Vector2(0, 0))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    private void PlayerRotation()
    {
        // Defines where should the player rotate
        Vector3 targetRotation = new Vector3(
            playerMesh.transform.eulerAngles.x,
            playerAngle,
            playerMesh.transform.eulerAngles.z
        );

        // Rotates the player
        transform.rotation = Quaternion.RotateTowards(
        transform.rotation,
        Quaternion.Euler(targetRotation),
        rotationSpeed * Time.deltaTime
        );
    }

    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (!context.canceled)
        {
            playerAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg;
        }
    }

    public void OnBark(InputAction.CallbackContext context)
    {

    }

    public void OnDig(InputAction.CallbackContext context)
    {

    }

    #endregion
}
