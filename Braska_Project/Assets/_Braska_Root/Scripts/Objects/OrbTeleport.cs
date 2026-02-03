using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrbTeleport : MonoBehaviour
{
    [Header("Orb Stats")]
    [SerializeField] int orbLevel;
    [SerializeField] float orbSpeed;

    [Header("Object references")]
    [SerializeField] GameObject portal;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "SCN_Lobby")
        {
            portal = GameObject.Find("PortalCenter");
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SCN_Lobby" && ScenesManager.instance.teleportedOrbs >= orbLevel)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (portal != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                portal.transform.position,
                orbSpeed * Time.deltaTime
            );
        }
    }
}
