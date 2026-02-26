using System;
using Unity.VisualScripting;
using UnityEngine;

public class RuneTriggerBird : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField] RuneBird bird;
    [SerializeField] BirdFollower birdFollower;


 
    public float timemove = 4.5f;



 

    private void OnTriggerEnter(Collider other)
    {
       
        if (!other.CompareTag("Bark")) return;

        if (!ObjectManager.instance.barkAvailable) return;
        if (!ObjectManager.instance.runeCanTrigger) return;
        UnlockAllPlatforms();

        birdFollower.isActive = true;
        birdFollower.playersee = true;
        if (bird.currentRune != this || !bird.waitingForBark) return;
        bird.StartMove();
        ObjectManager.instance.RunePrepareMove();
        Invoke(nameof(ResetBirdFollower), timemove);



        ShakeAllPlatforms();
    }
    private void ResetBirdFollower()
    {
        birdFollower.ResetRune();
    }


    private void UnlockAllPlatforms()
    {


        GameObject[] platforms = GameObject.FindGameObjectsWithTag("RunePlatform");

        foreach (var go in platforms)
        {
            RunePlatform platform = go.GetComponent<RunePlatform>();
            if (platform != null)
            {
                platform.UnlockRune();
            }
        }
    }
  
 


    #region ShakePlatforms

    private void ShakeAllPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("RunePlatform");
        foreach (var go in platforms)
        {
            RunePlatform platform = go.GetComponent<RunePlatform>();
            if (platform != null)
            {
                platform.TriggerShakeOnly(0.7f);
            }
        }
    }


    #endregion
}
