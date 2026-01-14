using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform targetTransform;
    [SerializeField] Vector3 worldOffset;

    [Header("Camera")]
    [SerializeField] float cameraSize = 4f;

    Camera mainCamera;
    Vector3 originalPosition;
    float originalCameraSize;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = targetTransform.position;
        originalCameraSize = mainCamera.orthographicSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetTransform.position += worldOffset;

            mainCamera.orthographicSize = cameraSize;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetTransform.position = originalPosition;

            mainCamera.orthographicSize = originalCameraSize;
        }
    }
}
