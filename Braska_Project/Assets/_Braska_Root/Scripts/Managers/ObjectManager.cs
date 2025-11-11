using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    [Header("Orb controls")]
    public bool hasOrb;
    [SerializeField] GameObject orb;

    [Header("Rune controls")]
    public bool runeCanTrigger;
    public bool runeCanMove;
    public bool runeOnPointA;
    public float runeMoveTime;
    public float runeCooldownTime;
    [SerializeField] Material matPedestal;
    [SerializeField] Material matRune;

    [Header("Geyser controls")]
    public float geyserOffset;
    public float geyserMoveTime;
    public float geyserCooldownTime;
    public bool geyserIsUp;

    private void Awake()
    {
        // Makes sure that there's always 1 instance.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        runeCanTrigger = true;
        runeCanMove = false;
        runeOnPointA = true;

        geyserIsUp = false;
        GeyserPosition();
    }

    public void LocateOrb()
    {
        orb = GameObject.Find("PF_Orb");

        if (orb != null)
        {
            Invoke(nameof(ShowOrb), 0.5f);
        }
    }

    private void ShowOrb()
    {
        if (!hasOrb)
        {
            Debug.Log("Particles!");
        }
    }

    #region Rune Functions

    public void RunePrepareMove()
    {
        runeCanTrigger = false;

        Invoke(nameof(RuneMove), runeCooldownTime);
    }

    private void RuneMove()
    {
        runeCanMove = true;

        Invoke(nameof(RuneStop), runeMoveTime * 8.25f + runeCooldownTime); // Stops the rune exactly when it reaches the other point + the cooldown.
    }

    public void RuneStop()
    {
        runeCanMove = false;
        runeOnPointA = !runeOnPointA;
        runeCanTrigger = true;
    }

    #endregion

    #region Geyser Functions

    public void GeyserPosition()
    {
        geyserIsUp = !geyserIsUp;

        Invoke(nameof(GeyserPosition), geyserCooldownTime);
    }

    #endregion
}
