using UnityEngine;

public class SoundWaterfall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InvokeRepeating(nameof(SoundWaterfallas), 60f, 60f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundWaterfallas()
    {
        AudioManager.Instance.PlaySFX (22);

    }
}
