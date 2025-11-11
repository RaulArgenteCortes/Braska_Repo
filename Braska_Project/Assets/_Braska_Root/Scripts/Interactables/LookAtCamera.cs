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
        targetRotatiom = new Vector3(
            transform.eulerAngles.x,
            axsis.transform.eulerAngles.y - 105,
            transform.eulerAngles.z
        );

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(targetRotatiom),
            rotationSpeed * Time.deltaTime
        );
    }
}
