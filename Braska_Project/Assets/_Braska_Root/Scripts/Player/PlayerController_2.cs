using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GridBrushBase;

public class CharacterController : MonoBehaviour
{
    [Header("Movement stats")]
    public float moveSpeed;
    public float moveX;
    public float moveZ;
    public Vector2 moveInput;
    /*public bool canMovePX;
    public bool canMoveNX;
    public bool canMovePZ;
    public bool canMoveNZ;*/

    [Header("References")]
    public Rigidbody playerRb;
    public GameObject groundCheck;
    public GameObject cam;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        groundCheck = GameObject.Find("GroundCheck");
    }

    private void Start()
    {

    }

    private void Update()
    {
        SetMoveDirection();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    private void OnTriggerStay(Collider other)
    {
        // Moves the player upwards when touching a ramp
        if (other.gameObject.layer == 6)
        {
            playerRb.transform.position += new Vector3(0, moveSpeed, 0);
        }
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
        playerRb.transform.position += new Vector3(moveX * moveSpeed, 0, moveZ * moveSpeed);
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
