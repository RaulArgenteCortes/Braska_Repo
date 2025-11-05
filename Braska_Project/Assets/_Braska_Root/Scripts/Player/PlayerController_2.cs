using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.GridBrushBase;

public class PlayerController_2 : MonoBehaviour
{
    [Header("Movement stats")]
    [SerializeField] bool canMove;
    [SerializeField] float moveSpeed;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] Vector2 moveInput; // Input from controller.

    [Header("Rotation stats")]
    [SerializeField] float playerAngle;
    [SerializeField] Vector3 targetRotation;
    [SerializeField] Vector3 meshTargetRotation;
    [SerializeField] float rotationSpeed;

    [Header("Actions stats")]
    public bool canBark;

    [Header("Border stats")]
    [SerializeField] GameObject borderCheckA;
    [SerializeField] GameObject borderCheckB;
    [SerializeField] float borderCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool groundAhead;

    [Header("Object references")]
    [SerializeField] GameObject playerMesh;
    [SerializeField] GameObject barkArea;
    private GameObject worldAxsis;

    private void Awake()
    {
        worldAxsis = GameObject.Find("PF_WorldAxsis");
    }

    private void Start()
    {
        barkArea.SetActive(false);

        canBark = true;
        canMove = true;

        SpawnTransform();
    }

    private void SpawnTransform() // Spawns the player where it should be.
    {
        transform.position = ScenesManager.instance.spawnPoint;

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            ScenesManager.instance.spawnView,
            transform.eulerAngles.z
        );       
    }

    private void Update()
    {
        CheckUpdate();
    }

    private void CheckUpdate() // Updates all terrain checks.
    {
        groundAhead =
            Physics.CheckSphere(borderCheckA.transform.position, borderCheckRadius, groundLayer) &&
            Physics.CheckSphere(borderCheckB.transform.position, borderCheckRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        PlayerRotation();

        PlayerMove();
    }

    private void PlayerRotation()
    {
        // Defines where should the player rotate.
        targetRotation = new Vector3(
            transform.eulerAngles.x,
            playerAngle + worldAxsis.transform.eulerAngles.y, // Adds the camera rotation.
            transform.eulerAngles.z
        );

        // Rotates the player.
        if (moveInput != new Vector2(0, 0) && canMove)
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
        if (moveInput != new Vector2(0, 0) && canMove && groundAhead) // Accelerates the player when it can and starts moving (and there's ground/slope).
        {
            // Prevents the player from going too fast.
            if (moveSpeed <= maxSpeed)
            {
                moveSpeed += accelerationSpeed;
            }
            else if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
        else if (!groundAhead) // Stops and deaccelerates the player when there is not terrain ahead.
        {
            moveSpeed = 0;
            moveSpeed -= accelerationSpeed * 1.1f;
        }
        else if (moveSpeed > 0) // Deaccelerates the player when it stops moving.
        {
            moveSpeed -= accelerationSpeed * 2f;
        }
        else if (moveSpeed != 0) // Prevents the player from moving while still.
        {
            moveSpeed = 0;
        }

        transform.position += moveSpeed * Time.deltaTime * transform.forward; // Moves the player forward.
    }

    private void StartBark()
    {
        if (canBark)
        {
            canBark = false;
            canMove = false;
            barkArea.SetActive(true);
            Invoke(nameof(FinishBark), 0.5f);
        }
    }

    private void FinishBark()
    {
        barkArea.SetActive(false);
        canMove = true;
        canBark = true;
    }

    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (!context.canceled)
        {
            playerAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg; // Transform the input vector 2 into a float .        
        }
    }

    public void OnBark(InputAction.CallbackContext context)
    {
        StartBark();
    }

    public void OnDig(InputAction.CallbackContext context)
    {

    }

    #endregion
}
