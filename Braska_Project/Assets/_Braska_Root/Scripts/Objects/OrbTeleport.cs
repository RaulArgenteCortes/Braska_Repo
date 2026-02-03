using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrbTeleport : MonoBehaviour
{
    [Header("Orb Stats")]
    [SerializeField] int orbLevel;
    [SerializeField] bool teleportTheOrb;

    [Header("Object references")]
    [SerializeField] ParticleSystem portalParticles;
    [SerializeField] GameObject portal;
    [SerializeField] GameObject orbFollow;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "SCN_Lobby")
        {
            portal = GameObject.Find("PortalCenter");
            orbFollow = GameObject.Find("OrbFollow");
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SCN_Lobby" && ScenesManager.instance.teleportedOrbs >= orbLevel)
        {
            gameObject.SetActive(false);
        }
        
        teleportTheOrb =
            orbLevel <= ScenesManager.instance.collectedOrbs;
    }

    private void FixedUpdate()
    {
        if (portal != null && teleportTheOrb && orbLevel != 4)
        {
            transform.SetParent(null);

            transform.position = Vector3.MoveTowards(
                transform.position,
                portal.transform.position,
                2 * Time.deltaTime
            );

            if (transform.position.z > 7.5f)
            {
                Debug.Log("pain2");

                portalParticles.transform.position = transform.position;
                portalParticles.Play();

                ScenesManager.instance.teleportedOrbs += 1;
                gameObject.SetActive(false);
            }
        }
        else if (orbFollow != null && orbLevel == 4 && ScenesManager.instance.collectedOrbs == 4)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                orbFollow.transform.position,
                5 * Time.deltaTime
            );
        }
    }
}
