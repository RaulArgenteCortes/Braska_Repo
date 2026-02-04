using UnityEngine;

public class SoundWaterfall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(SoundWaterfallas), 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundWaterfallas()
    {
        AudioManager.Instance.PlayMusic (2);

    }
}
