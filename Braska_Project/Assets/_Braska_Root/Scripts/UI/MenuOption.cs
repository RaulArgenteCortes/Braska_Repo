using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider sfxSlider;
    
    void Start()
    {
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);

        MusicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;


        MusicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }
    public void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    public void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }

    }
