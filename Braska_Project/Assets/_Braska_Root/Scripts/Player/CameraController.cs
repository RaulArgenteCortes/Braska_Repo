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

    [Header("Rotación Suave")]
    bool isLerping = false;
    float lerpTargetY;
    public float lerpSpeed = 100f;
    bool allowInput = true;

    [Header("Zoom (Orthographic)")]
    [SerializeField] float zoomSpeed = 8f;
    [SerializeField] float minZoom = 4f;
    [SerializeField] float maxZoom = 14f;

    float zoomInput;
    [SerializeField] Camera cam;

    private void Start()
    {
        cameraRotation = startingRotation;
        if (cam == null)
        {
            Debug.LogError("CameraController: Camera no asignada");
            enabled = false;
            return;
        }

        cam.orthographic = true;

    }

    private void Update()
    {
        TargetRotation();
        HandleZoom();
    }
    void HandleZoom()
    {
        if (Mathf.Abs(zoomInput) < 0.01f) return;

        cam.orthographicSize -= zoomInput * zoomSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }

    
    private void TargetRotation()
    {
        if (rotationInput > 0)
        {
            rotationTargetSpeed = rotationMaxSpeed;
        }
        if (rotationInput < 0)
        {
            rotationTargetSpeed = -rotationMaxSpeed;
        }
        if (rotationInput == 0)
        {
            rotationTargetSpeed = 0;
        }
        if (!allowInput)
        {
            rotationTargetSpeed = 0;
            return;
        }
    }
    public void LockRotation()
    {
        allowInput = false;
       
    }

    public void UnlockRotation()

    {
        allowInput = true;

    }
    public void LerpToRotation(float yRotation)
    {
        lerpTargetY = yRotation;
        isLerping = true;
        allowInput = false; 
    }
    private void FixedUpdate()
    {
        if (isLerping)
        {
            Quaternion currentRot = Quaternion.Euler(0f, cameraRotation, 0f);
            Quaternion targetRot = Quaternion.Euler(0f, lerpTargetY, 0f);

            currentRot = Quaternion.RotateTowards(currentRot, targetRot, lerpSpeed * Time.fixedDeltaTime);

            cameraRotation = currentRot.eulerAngles.y;

            if (Quaternion.Angle(currentRot, targetRot) < 0.1f)
            {
                cameraRotation = lerpTargetY;
                isLerping = false;
                allowInput = false;
            }
        }
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

        if (cameraRotation > 360)
        {
            cameraRotation -= 360;
        }
        if (cameraRotation < 0)
        {
            cameraRotation += 360;
        }
    }

    #region Input Methods

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<float>();
    }
    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        zoomInput = context.ReadValue<float>();
    }

    #endregion

}