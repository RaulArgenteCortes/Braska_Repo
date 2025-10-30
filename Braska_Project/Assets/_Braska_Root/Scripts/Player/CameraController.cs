using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera stats")]
    public float startingRotation;
    public float cameraRotation; // Current rotation of the camera
    public float rotationAcceleration;
    public float rotationMaxSpeed;
    public float rotationSpeed;
    public float rotationInput;
    public float rotationDirection;

    void Start()
    {
        cameraRotation = startingRotation;
    }

    void Update()
    {
        RotateCamera();
    }

    void FixedUpdate()
    {
        // Actualiza la rotación de Y con cameraRotation
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            cameraRotation,
            transform.eulerAngles.z
        );
    }

    void RotateCamera()
    {
        if (rotationInput != 0)
        {
            if (rotationSpeed < rotationMaxSpeed && rotationSpeed > -rotationMaxSpeed)
            {
                rotationSpeed += rotationAcceleration * rotationDirection;
            }
        }
        else
        {
            if (rotationSpeed > rotationAcceleration)
            {
                rotationSpeed += rotationAcceleration * -rotationDirection;
            }
            else if (rotationSpeed < -rotationAcceleration)
            {
                rotationSpeed += rotationAcceleration * -rotationDirection;
            }
            else
            {
                rotationSpeed = 0;
            }
        }

        if (rotationSpeed > rotationMaxSpeed && rotationSpeed < -rotationMaxSpeed)
        {
            rotationSpeed = rotationMaxSpeed * rotationDirection;
        }

        // Modifica cameraRotation
        cameraRotation += rotationSpeed;
    }

    #region Input Methods

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<float>();

        if (rotationInput > 0)
        {
            rotationDirection = 1;
        }
        else if (rotationInput < 0)
        {
            rotationDirection = -1;
        }
    }

    #endregion
}