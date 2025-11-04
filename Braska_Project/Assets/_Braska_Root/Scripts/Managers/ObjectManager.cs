using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    [Header("Rune stats")]
    public bool runeCanTrigger;
    public bool runeCanMove;
    public bool runeOnPointA;
    public float runeMoveTime;
    public float runeCooldownTime;

    [Header("Geyser stats")]
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

        Invoke(nameof(RuneStop), runeMoveTime * 4 + runeCooldownTime);
    }

    private void RuneStop()
    {
        runeCanMove = false;
        runeOnPointA = !runeOnPointA;
        runeCanTrigger = true;
    }

    #endregion

    #region Rune Functions

    public void GeyserPosition()
    {
        geyserIsUp = !geyserIsUp;

        Invoke(nameof(GeyserPosition), geyserCooldownTime);
    }

    #endregion
}
