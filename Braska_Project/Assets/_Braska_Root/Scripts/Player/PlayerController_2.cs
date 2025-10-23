using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.GridBrushBase;

public class CharacterController : MonoBehaviour
{
    [Header("Movement stats")]
    public float moveSpeed; // Current speed
    public float maxMoveSpeed; // Max speed
    public Vector2 moveInput;

    [Header("Rotation stats")]
    public float playerAngle;
    public Vector3 targetRotation;
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
        
    }

    private void FixedUpdate()
    {
        PlayerRotation();

        PlayerMove();
    }

    private void PlayerRotation()
    {
        // Defines where should the player rotate
        targetRotation = new Vector3
        (
            transform.eulerAngles.x,
            playerAngle,
            transform.eulerAngles.z
        );

        // Rotates the player
        transform.rotation = Quaternion.RotateTowards
        (
            transform.rotation,
            Quaternion.Euler(targetRotation),
            rotationSpeed * Time.deltaTime
        );
    }

    private void PlayerMove()
    {
        if (moveInput != new Vector2(0, 0))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            if (moveSpeed <= maxMoveSpeed)
            {
            moveSpeed += 0.1f;
            }
        }
    }

    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (!context.canceled)
        {
            playerAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg // Transform the input vector 2 into a float
                + worldAxsis.transform.eulerAngles.y; // Adds the camera rotation
        }
        else
        {
            moveSpeed = 0;
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
