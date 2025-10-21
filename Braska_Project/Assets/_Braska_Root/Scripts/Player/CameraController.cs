using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float cameraRotation;
    [SerializeField] float rotationChange;
    [SerializeField] float rotationDirection;

    void Start()
    {
        rotationDirection = 0;
    }

    void Update()
    {
        RotateCamera();
    }

    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            cameraRotation,
            transform.eulerAngles.z
        );
    }

    void RotateCamera()
    {
        cameraRotation += rotationChange * rotationDirection;
    }

    #region Input Methods

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        rotationDirection = context.ReadValue<float>();
    }

    #endregion
}