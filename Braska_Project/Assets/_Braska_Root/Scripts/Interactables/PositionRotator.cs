using UnityEngine;
using UnityEngine.XR;

public class PositionRotator : MonoBehaviour
{
    [Header("Rotation stats")]
    [SerializeField] float minRotation;

    void Start()
    {
        // Rota el mesh dependiendo de su posición.
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            (transform.position.x + transform.position.y + transform.position.z) * minRotation - 45,
            transform.eulerAngles.z
        );
    }
}
