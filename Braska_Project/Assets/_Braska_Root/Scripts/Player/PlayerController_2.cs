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
        SetMoveDirection();
    }

    private void FixedUpdate()
    {
        PlayerMove();

        PlayerRotate();
    }

    private void SetMoveDirection() // Sets de move direction of the player
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
    }

    private void PlayerMove()
    {
        playerRb.transform.localPosition += new Vector3(moveX * moveSpeed, 0, moveZ * moveSpeed);
    }

    private void PlayerRotate()
    {
        // Controls mesh rotation regardless of its parent
        playerMesh.transform.eulerAngles = new Vector3(
            playerMesh.transform.eulerAngles.x,
            0,
            playerMesh.transform.eulerAngles.z
        );
    }

    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnBark(InputAction.CallbackContext context)
    {

    }

    public void OnDig(InputAction.CallbackContext context)
    {

    }

    #endregion
}
