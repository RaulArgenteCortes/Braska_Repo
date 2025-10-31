using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Rotation stats")]
    public float startingRotation;
    public float cameraRotation; // Current rotation of the camera
    [SerializeField] float rotationMaxSpeed;
    [SerializeField] float rotationTargetSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationInput;

    private void Start()
    {
        cameraRotation = startingRotation;
    }

    private void Update()
    {
        TargetRotation();
    }

    private void TargetRotation()
    {
        if (rotationInput > 0)
        {
            rotationTargetSpeed = rotationMaxSpeed;
        }
        else if (rotationInput < 0)
        {
            rotationTargetSpeed = -rotationMaxSpeed;
        }
        else
        {
            rotationTargetSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        RotateCamera();

        // Updates Y with cameraRotation
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            cameraRotation,
            transform.eulerAngles.z
        );
    }

    private void RotateCamera()
    {
        // Gradually increases rotationSpeed
        rotationSpeed = Mathf.Lerp(rotationSpeed, rotationTargetSpeed, Time.deltaTime * rotationMaxSpeed * 2);

        if (rotationSpeed < 0.1 && rotationSpeed > -0.1 && rotationInput == 0)
        {
            rotationSpeed = 0;
        }

        // Modifies cameraRotation
        cameraRotation += rotationSpeed;
    }

    #region Input Methods

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<float>();
    }

    #endregion
}