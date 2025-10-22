using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera stats")]
    [SerializeField] float startingRotation; // Rotación inicial
    public float cameraRotation; // Rotación actual
    [SerializeField] float rotationSpeed; // Velocidad de rotación
    [SerializeField] float rotationDirection; // Dirección hacia donde rota la cámara

    [Header("Camera stats")]
    public GameObject player;
    public Vector3 playerPos;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

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
        playerPos = player.transform.localPosition;

        // Modifica cameraRotation
        cameraRotation += rotationSpeed * rotationDirection;

        player.transform.localPosition = playerPos;
    }

    #region Input Methods

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        rotationDirection = context.ReadValue<float>();
    }

    #endregion
}