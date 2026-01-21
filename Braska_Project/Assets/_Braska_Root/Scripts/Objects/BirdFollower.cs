using UnityEngine;

public class BirdFollower : MonoBehaviour
{
    [Header("References")]
    public RuneBird targetBird; // el pájaro principal a seguir
    public float followDelay = 0.5f; // cuánto se retrasa (en segundos)
    public float speed = 3f; // velocidad de seguimiento

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
}
