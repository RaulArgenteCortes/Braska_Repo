using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region "Variables"
    [Header("Movimiento")]
    [SerializeField] float speed = 5f;

    [Header("Acciones")]
    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Transform cam;

    [Header("Slope Handling")]
    [SerializeField] private float maxSlopeAngle = 45f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float slopeRayLength = 1.5f;
    private RaycastHit slopeHit;
    #endregion
    #region "Actualizaciones"
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        cam = Camera.main.transform;

    }

    private void FixedUpdate()
    {
        Move();
    }

    #endregion
    #region "Variable Move y slope"
    public void Move()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0f, input.y);

        // Movimiento relativo a la cámara
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();
        Vector3 moveDirection = (camForward * move.z + camRight * move.x).normalized;

        Vector3 velocity = rb.linearVelocity;
        Vector3 horizontalVelocity = moveDirection * speed;

        if (OnSlope())
        {
            // Adapatacion en pendiente 
            rb.linearVelocity = Vector3.ProjectOnPlane(horizontalVelocity, slopeHit.normal);

            // Añadir fuerza hacia abajo
            if (rb.linearVelocity.y > -0.1f)
                rb.AddForce(Vector3.down * 10f, ForceMode.Force);
        }
        else
        {
            // Movimiento suelo plano
            rb.linearVelocity = new Vector3(horizontalVelocity.x, velocity.y, horizontalVelocity.z);
        }

        // Rotación suave hacia el move
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime);
        }
    }
    private bool OnSlope()
    {
        // Raycast hacia abajo del jugador
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2f + slopeRayLength))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle > 0f && angle <= maxSlopeAngle;
        }
        return false;

    }
    #endregion
}




