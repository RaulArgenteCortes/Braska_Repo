using UnityEngine;

public class SlopeTilt : MonoBehaviour // lee la rotacion del jugador de 0 a 1. Réstale 0.5 y multiplicalo por 45*2. Usa el resultado en la rotación del mesh.
{
    [Header("Tilt stats")]
    public Vector3 targetTilt;
    public float XTilt;
    public float tiltSpeed;

    [Header("Other rotations")]
    public float slopeRotation;
    [SerializeField] float playerRotation;

    [Header("Object references")]
    [SerializeField] GameObject playerMesh;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slope"))
        {
            targetTilt = new Vector3(XTilt, 0, 0);

            slopeRotation = other.transform.eulerAngles.y;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slope"))
        {
            targetTilt = Vector3.zero;
        }
    }

    private void Update()
    {
        ChangeTilt();
    }

    private void ChangeTilt()
    {
        playerRotation = Quaternion.Angle(transform.parent.rotation, Quaternion.Euler(0, slopeRotation, 0));

        XTilt = ((playerRotation + -90) / 90) * -45;
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
