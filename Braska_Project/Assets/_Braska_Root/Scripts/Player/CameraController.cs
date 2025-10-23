using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera stats")]
    public float startingRotation; // Rotaci�n inicial
    public float cameraRotation; // Rotaci�n actual
    public float rotationSpeed; // Velocidad de rotaci�n
    public float rotationDirection; // Direcci�n hacia donde rota la c�mara

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
        // Actualiza la rotaci�n de Y con cameraRotation
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            cameraRotation,
            transform.eulerAngles.z
        );
    }

    void RotateCamera()
    {
        // Modifica cameraRotation
        cameraRotation += rotationSpeed * rotationDirection;
    }

    #region Input Methods

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        rotationDirection = context.ReadValue<float>();
    }

    #endregion
}