using UnityEngine;

public class SlopeTilt : MonoBehaviour // lee la rotacion del jugador de 0 a 1. Réstale 0.5 y multiplicalo por 45*2. Usa el resultado en la rotación del mesh.
{
    [Header("Tilt stats")]
    public Vector3 targetTilt;
    public float XTilt;
    public float tiltSpeed;

    [SerializeField] float playerRotation;

    [Header("Slope stats")]
    public float slopeRotation;

    [Header("Object references")]
    public GameObject playerMesh;

    private void Awake()
    {
        playerMesh = GameObject.Find("PlayerMesh");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            targetTilt = new Vector3(
                XTilt,
                0,
                0
            );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            targetTilt = new Vector3(
                0,
                0,
                0
            );
        }
    }

    private void Update()
    {
        ChangeTilt();
    }

    private void ChangeTilt()
    {
        if (transform.parent.eulerAngles.y == 0)
        {
            playerRotation = 360;
        }
        else
        {
            playerRotation = transform.parent.eulerAngles.y;
        }

        XTilt = (playerRotation - 180);
    }

    private void FixedUpdate()
    {
        MeshTilt();
    }

    private void MeshTilt()
    {
        if (playerMesh.transform.eulerAngles != targetTilt)
        {
            playerMesh.transform.localRotation = Quaternion.RotateTowards
            (
                playerMesh.transform.localRotation,
                Quaternion.Euler(targetTilt),
                tiltSpeed * Time.deltaTime
            );
        }
    }
}
