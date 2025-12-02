using UnityEditor;
using UnityEngine;

public class OrbFollow : MonoBehaviour
{
    [Header("Progress stats")]
    public int currentLevel;

    [Header("Follow stats")]
    [SerializeField] bool followStart = false;
    [SerializeField] float followSpeed;

    [Header("Object references")]
    public GameObject orbFollow;
    public GameObject teleportLobby;
    public GameObject DigVFX;
    public float timevfx = 1f;
    

    private void Awake()
    {
        orbFollow = GameObject.Find("OrbFollow");
    }

    private void Start()
    {
        if (currentLevel <= ScenesManager.instance.collectedOrbs)
        {
            gameObject.SetActive(false);
        }
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
        if (other.gameObject.CompareTag("Dig") && !ObjectManager.instance.hasOrb)
        {
            ObjectManager.instance.hasOrb = true;

            AudioManager.Instance.PlaySFX(2);

            Invoke(nameof(FollowStart), 1f);

            if (DigVFX != null)
            {
                Vector3 spawnPos = transform.position + new Vector3(0, -0.25f, 0);

                GameObject vfx = Instantiate(DigVFX, spawnPos, Quaternion.identity);

                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    Vector3 backDir = -player.transform.forward;
                    vfx.transform.rotation = Quaternion.LookRotation(backDir);
                }

                Destroy(vfx, timevfx);
            }
        }
    }
    

    private void FollowStart()
    {
        AudioManager.Instance.PlaySFX(3);
        followStart = true;
    }
}
