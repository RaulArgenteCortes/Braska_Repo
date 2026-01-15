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

    [Header("Material")]
    [SerializeField] private Renderer orbRenderer;
    [SerializeField] private Renderer orbRenderer2;
    private Material orbMaterial;
    private Material orbMaterial2;

    public Color orbColorLVl1;
    public Color orbColorLVl2;
    public Color orbColorLVl3;
    public Color orbColorLVl4;

    [Header("Emission Settings")]
    [SerializeField] private float emissionIntensity = 2f;

    private void Awake()
    {
        orbFollow = GameObject.Find("OrbFollow");
       

    }

    private void Start()
    {
        if (orbRenderer == null)
            orbRenderer = GetComponent<Renderer>();
        orbMaterial = orbRenderer.material;
        orbMaterial2 = orbRenderer2.material;

        SetEmissionByLevel();

        if (currentLevel <= ScenesManager.instance.collectedOrbs)
        {
            gameObject.SetActive(false);
        }


    }
    private void SetEmissionByLevel()
    {
        Color emissionColor = Color.black;

        switch (currentLevel)
        {
            case 1:
                emissionColor = orbColorLVl1;
                break;

            case 2:
                emissionColor = orbColorLVl2;
                break;

            case 3:
                emissionColor = orbColorLVl3;
                break;

            case 4:
                emissionColor = orbColorLVl4;
                break;
        }

        // Activar emisión
        orbMaterial.EnableKeyword("_EMISSION");
        orbMaterial2.EnableKeyword("_EMISSION");
        orbMaterial.SetColor("_EmissionColor", emissionColor * emissionIntensity);
        orbMaterial2.SetColor("_EmissionColor", emissionColor * emissionIntensity);
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
