using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform targetTransform;
    [SerializeField] Vector3 worldOffset;

    [Header("Camera")]
    [SerializeField] float cameraSize = 2f;
    [SerializeField] float targetYRotation;


    Camera mainCamera;
    Vector3 originalPosition;
    float originalCameraSize;
    CameraController cam;
    float originalYRotation;


    void Start()
    {
        mainCamera = Camera.main;
        cam = targetTransform.GetComponent<CameraController>();

        originalPosition = targetTransform.position;
        originalCameraSize = mainCamera.orthographicSize;
        originalYRotation = cam.cameraRotation;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetTransform.position += worldOffset;
            cam.SetRotation(targetYRotation);
            mainCamera.orthographicSize = cameraSize;
        }
    }

  
   

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetTransform.position = originalPosition;

            cam.SetRotation(originalYRotation);

            mainCamera.orthographicSize = originalCameraSize;
        }
    }
}
