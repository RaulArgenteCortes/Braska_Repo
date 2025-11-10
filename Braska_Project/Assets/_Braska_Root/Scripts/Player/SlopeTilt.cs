using UnityEngine;

public class SlopeTilt : MonoBehaviour // lee la rotacion del jugador de 0 a 1. Réstale 0.5 y multiplicalo por 45*2. Usa el resultado en la rotación del mesh.
{
    [Header("Tilt stats")]
    public Vector3 tiltTarget;
    public float tiltX;
    public float tiltSpeed;
    public float tiltMax;

    [Header("Other rotations")]
    public float slopeRotation;
    [SerializeField] float playerRotation;

    [Header("Object references")]
    [SerializeField] GameObject playerMesh;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slope"))
        {
            tiltTarget = new Vector3(tiltX, 0, 0);

            slopeRotation = other.transform.eulerAngles.y;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slope"))
        {
            tiltTarget = Vector3.zero;
        }
    }

    private void Update()
    {
        ChangeTilt();
    }

    private void ChangeTilt()
    {
        playerRotation = Quaternion.Angle(transform.parent.rotation, Quaternion.Euler(0, slopeRotation, 0));

        tiltX = ((playerRotation + -90) / 90) * -tiltMax;
    }

    private void FixedUpdate()
    {
        MeshTilt();
    }

    private void MeshTilt()
    {
        if (playerMesh.transform.eulerAngles != tiltTarget)
        {
            playerMesh.transform.localRotation = Quaternion.RotateTowards
            (
                playerMesh.transform.localRotation,
                Quaternion.Euler(tiltTarget),
                tiltSpeed * Time.deltaTime
            );
        }
    }
}
