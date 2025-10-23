using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.GridBrushBase;

public class CharacterController : MonoBehaviour
{
    [Header("Movement stats")]
    public float moveSpeed;
    public float accelerationSpeed;
    public float maxSpeed;
    public Vector2 moveInput; // Input from controller

    [Header("Rotation stats")]
    public float playerAngle;
    public Vector3 targetRotation;
    public float rotationSpeed;
    // Stats for slopes
    [SerializeField] GameObject slopeCheck;
    [SerializeField] float slopeCheckRadious;
    [SerializeField] LayerMask slopeLayer;
    [SerializeField] bool isOnSlope;

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
            playerAngle + worldAxsis.transform.eulerAngles.y, // Adds the camera rotation
            transform.eulerAngles.z
        );

        // Rotates the player
        if (moveInput != new Vector2(0, 0))
        {
            transform.rotation = Quaternion.RotateTowards
            (
                transform.rotation,
                Quaternion.Euler(targetRotation),
                rotationSpeed * Time.deltaTime
            );
        } 
    }

    private void PlayerMove()
    {
        if (moveInput != new Vector2(0, 0))
        {
            if (moveSpeed <= maxSpeed)
            {
            moveSpeed += accelerationSpeed;
            }
        }
        else if (moveSpeed > 0)
        {
            moveSpeed -= accelerationSpeed * 2;
        }
        else if (moveSpeed != 0)
        {
            moveSpeed = 0;
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime; // Moves the player forward
    }

    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (!context.canceled)
        {
            playerAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg; // Transform the input vector 2 into a float         
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
