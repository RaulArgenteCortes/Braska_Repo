using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [Header("Spawn stats")]
    [SerializeField] string sceneToLoad;
    [SerializeField] string newSpawnTeleport;

    [Header("Status stats")]
    [SerializeField] bool playerInside;
    [SerializeField] int requiredOrbs;
    public bool isActive;

    [Header("Render Stats")]
    private float targetEmissionIntensity = 0;
    private float currentEmissionIntensity = 0;
    [SerializeField] Renderer teleportRenderer;

    [Header("Object references")]
    [SerializeField] GameObject teleportLight;
    [SerializeField] GameObject teleportParticles;

    private void Awake()
    {
        teleportParticles.SetActive(false);
    }

    private void Start()
    {
        playerInside = false;

        if (ScenesManager.instance.collectedOrbs < requiredOrbs)
        {
            isActive = false;
        }

        if (ScenesManager.instance.collectedOrbs == requiredOrbs)
        {
            Highlight();
        }

        if (!isActive)
        {
            teleportLight.SetActive(false);
        }
    }

    private void Update()
    {
        if (ObjectManager.instance.hasOrb)
        {
            Invoke(nameof(Highlight), 0.5f);
        }
    }

    private void FixedUpdate()
    {
        Shine();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && playerInside && ObjectManager.instance.barkAvailable)
        {
            OrderTeleport();

            if (ScenesFade.Instance != null)
            {
                ScenesFade.Instance.PlayTeleportVFX(transform.position);
            }
        }
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void OrderTeleport()
    {
        AudioManager.Instance.PlaySFX(9);
        ScenesManager.instance.TeleportPlayer(sceneToLoad, newSpawnTeleport);
    }

    public void Shine()
    {
        if (playerInside && isActive)
        {
            targetEmissionIntensity = 4;
        }
        else if (isActive)
        {
            targetEmissionIntensity = 1;
        }
        else
        {
            targetEmissionIntensity = 0;
        }

        currentEmissionIntensity = Mathf.MoveTowards(
            currentEmissionIntensity,
            targetEmissionIntensity,
            Time.fixedDeltaTime * ObjectManager.instance.prebarkEmissionSpeed
        );

        teleportRenderer.material.SetColor("_EmissionColor", Color.green * currentEmissionIntensity);
    }

    public void Highlight()
    {
        teleportParticles.SetActive(true);
    }
}
