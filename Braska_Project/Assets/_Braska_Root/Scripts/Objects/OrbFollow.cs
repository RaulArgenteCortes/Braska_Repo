using UnityEngine;

public class OrbFollow : MonoBehaviour
{
    [Header("Progress stats")]
    [SerializeField] int level;

    [Header("Follow stats")]
    [SerializeField] bool followStart = false;
    [SerializeField] float followSpeed;

    [Header("Object references")]
    public GameObject orbFollow;

    private void Awake()
    {
        if (level <= ScenesManager.instance.collectedOrbs)
        {
            gameObject.SetActive(false);
        }

        orbFollow = GameObject.Find("OrbFollow");
    }

    private void FixedUpdate()
    {
        if (followStart)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                orbFollow.transform.position,
                followSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dig"))
        {
            ObjectManager.instance.hasOrb = true;
            Invoke(nameof(FollowStart), 1f);
        }
    }

    private void FollowStart()
    {
        followStart = true;
    }
}
