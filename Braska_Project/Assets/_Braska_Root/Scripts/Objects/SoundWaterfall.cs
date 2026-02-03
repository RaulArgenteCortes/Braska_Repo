using UnityEngine;

public class SoundWaterfall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySFX(11);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
