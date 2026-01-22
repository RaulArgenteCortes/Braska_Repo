using UnityEngine;

public class BirdFollower : MonoBehaviour
{
    [Header("References")]
    public RuneBird targetBird;
    public float followDelay = 0.5f;
    public float speed = 20f;
    [SerializeField] float rotationSpeedFlying = 1f; // velocidad normal al volar
    [SerializeField] float rotationSpeedIdle = 1f;  // grados/segundo cuando está en A o C

    private Vector3 lastTargetPosition;

    void Start()
    {
        if (targetBird != null)
            lastTargetPosition = targetBird.transform.position;
    }

    void Update()
    {
        if (targetBird == null) return;

        // Calcula la posición atrasada del pájaro principal
        Vector3 targetPos = targetBird.transform.position;

        // Lerp hacia la posición del pájaro con un poco de retraso
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (targetBird == null) return;

        Transform lookTarget = targetBird.GetLookTarget();
        if (lookTarget == null) return;

        Vector3 direction = lookTarget.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 euler = lookRotation.eulerAngles;
        euler.x = 0f;
        euler.z = 0f;
        euler.y -= 90f; // offset según tu modelo

        Quaternion finalRotation = Quaternion.Euler(euler);

        // Decide la velocidad: rápida al volar, lenta en A/C
        float step = targetBird.moving ? rotationSpeedFlying : rotationSpeedIdle;

        // Giro suave usando RotateTowards (grados por segundo)
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            finalRotation,
            step * Time.deltaTime
        );
    }
}
