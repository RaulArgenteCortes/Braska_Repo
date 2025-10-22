using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera stats")]
    [SerializeField] float startingRotation; // Rotaci�n inicial
    public float cameraRotation; // Rotaci�n actual
    [SerializeField] float rotationSpeed; // Velocidad de rotaci�n
    [SerializeField] float rotationDirection; // Direcci�n hacia donde rota la c�mara

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
        // Actualiza la rotaci�n de Y con cameraRotation
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