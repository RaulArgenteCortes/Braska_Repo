using UnityEngine;

public class CollectibleAppear : MonoBehaviour
{
    public GameObject Collectible;
    public int requiredAmount;

    void Start()
    {
        if (ScenesManager.instance.collectedOrbs < requiredAmount) // Deactivates the collectible if the necessary amount hasn't been reached
        {
            Collectible.SetActive(false);
        }
    }
}
