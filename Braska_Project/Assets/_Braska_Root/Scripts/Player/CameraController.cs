using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera stats")]
    [SerializeField] float startingRotation; // Rotación inicial
    public float cameraRotation; // Rotación actual
    [SerializeField] float rotationSpeed; // Velocidad de rotación
    [SerializeField] float rotationDirection; // Dirección hacia donde rota la cámara

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