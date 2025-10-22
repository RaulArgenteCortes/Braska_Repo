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
    /*public bool canMovePX;
    public bool canMoveNX;
    public bool canMovePZ;
    public bool canMoveNZ;*/

    [Header("Ramp behavior")]
    public GameObject rampCheck;
    [SerializeField] float rampCheckRadius;
    [SerializeField] LayerMask rampLayer;
    [SerializeField] bool isOnRamp;

    [Header("References")]
    public Rigidbody playerRb;
    public GameObject playerMesh;
    public GameObject worldAxsis;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        rampCheck = GameObject.Find("RampCheck");
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

        RampCheck();
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
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            0 + worldAxsis.transform.eulerAngles.y,
            transform.eulerAngles.z
        );

        playerMesh.transform.eulerAngles = new Vector3(
            playerMesh.transform.eulerAngles.x,
            0,
            playerMesh.transform.eulerAngles.z
        );
    }

    private void RampCheck()
    {
        isOnRamp = Physics.CheckSphere(rampCheck.transform.position, rampCheckRadius, rampLayer);

        if (isOnRamp)
        {
            playerRb.useGravity = false;

            playerRb.angularVelocity = new Vector3(0, 0, 0);
        }
        else
        {
            playerRb.useGravity = true;
        }
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
