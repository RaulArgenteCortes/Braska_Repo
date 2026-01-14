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
    bool allowInput = true;

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
        if (!allowInput)
        {
            rotationTargetSpeed = 0;
            return;
        }
    }
    public void LockRotation(float yRotation)
    {
        allowInput = false;
        SetRotation(yRotation);
    }

    public void UnlockRotation()
    {
        allowInput = true;
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
    public void SetRotation(float yRotation)
    {
        cameraRotation = yRotation;
        rotationSpeed = 0;
        rotationTargetSpeed = 0;
    }

    private void RotateCamera()
    {
        if (!allowInput) return;
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