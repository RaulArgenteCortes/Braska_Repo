using UnityEngine;
using UnityEngine.XR;

public class PositionRotator : MonoBehaviour
{
    void Start()
    {
        // Rota el mesh dependiendo de su posición.
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            (transform.position.x + transform.position.y + transform.position.z) * 45,
            transform.eulerAngles.z
        );
    }
}
