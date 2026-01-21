using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [Header("Rotation stats")]
    [SerializeField] Vector3 targetRotatiom;
    [SerializeField] float rotationSpeed;

    [Header("Object references")]
    [SerializeField] GameObject axsis;

    private void Awake()
    {
        axsis = GameObject.Find("PF_WorldAxsis");
    }

    private void FixedUpdate()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            axsis.transform.eulerAngles.y - 105,
            transform.eulerAngles.z
        );
    }
}
