using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    #region"Variables
    [Header("Audio Source References")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;


    [Header("Audio Clip Arrays")]
    public AudioClip[] musiclist;
    public AudioClip[] sfxList;
    #endregion
    #region Void
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 1f);
            float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);

            SetMusicVolume(savedMusic);
            SetSFXVolume(savedSFX);
        }    
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    #region Musica&SFX
    public void PlayMusic(int musicIndex)
    {
        if (musicIndex < 0 || musicIndex >= musiclist.Length)
            return;

        AudioClip newClip = musiclist[musicIndex];

        if (musicSource.clip == newClip && musicSource.isPlaying)
            return;

        musicSource.clip = newClip;
        musicSource.loop = true; 
        musicSource.Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        sfxSource.PlayOneShot(sfxList[sfxIndex]);
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
    #endregion
}
