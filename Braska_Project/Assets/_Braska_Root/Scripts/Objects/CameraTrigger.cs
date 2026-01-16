using UnityEngine;
 

public class CameraTrigger : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform targetTransform;
    [SerializeField] Vector3 worldOffset;

    [Header("Camera")]
    [SerializeField] float cameraSize = 2.5f;
    [SerializeField] float targetYRotation;
    [SerializeField] float zoomDuration = 1f;



    Camera mainCamera;
    CameraController cam;

    bool isZooming = false;
    float zoomProgress = 0f;


    Vector3 startPos;
    Vector3 targetPos;
    float startSize;
    float targetSize;
    float startRot;
    float endRot;

    Vector3 originalPosition;
    float originalSize;

    void Start()
    {
        mainCamera = Camera.main;
        cam = targetTransform.GetComponent<CameraController>();

        originalPosition = targetTransform.position;
        originalSize = mainCamera.orthographicSize;

    }
    void Update()
    {
        if (isZooming)
        {
            zoomProgress += Time.deltaTime / zoomDuration;
            zoomProgress = Mathf.Clamp01(zoomProgress);

            float t = zoomProgress * zoomProgress * (3f - 2f * zoomProgress);

            targetTransform.position = Vector3.Lerp(startPos, targetPos, t);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            cam.cameraRotation = Mathf.LerpAngle(startRot, endRot, t);

            if (zoomProgress >= 1f)
            {
                targetTransform.position = targetPos;
                mainCamera.orthographicSize = targetSize;
                cam.cameraRotation = endRot;
                isZooming = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          
            StartZoom(targetTransform.position + worldOffset, cameraSize, targetYRotation);
            cam.LockRotation();

        }
    }

  
   

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.UnlockRotation();

            StartZoom(originalPosition, originalSize, cam.cameraRotation);
        }


    }
    private void StartZoom(Vector3 newPos, float newSize, float newRot)
    {
        startPos = targetTransform.position;
        targetPos = newPos;

        startSize = mainCamera.orthographicSize;
        targetSize = newSize;

        startRot = cam.cameraRotation;
        endRot = newRot;

        zoomProgress = 0f;
        isZooming = true;
    }
}

