using UnityEngine;

public class Músicalvl0 : MonoBehaviour
{
    public int musicID2 = 6;   
    public int musicID = 2;
    void Start()
    {
        AudioSource musicSource = AudioManager.Instance.GetComponent<AudioSource>();

        bool originalLoop = musicSource.loop;
        musicSource.loop = false;
        AudioManager.Instance.PlayMusic(musicID2);
        musicSource.loop = originalLoop;

        float clipDuration = AudioManager.Instance.musiclist[musicID2].length;
        Invoke("PlayLoopMusic", clipDuration);
    }
    void Update()
    {
        
    }
    void PlayLoopMusic()
    {
        AudioManager.Instance.PlayMusic(musicID);
    }
}
