using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    [Header("Rune stats")]
    public bool runeCanTrigger;
    public bool runeCanMove;
    public float runeMoveTime;
    public float runeCooldownTime;
    public bool runeOnPointA;

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
}
