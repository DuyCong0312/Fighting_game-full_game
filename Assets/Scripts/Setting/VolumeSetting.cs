using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Slider sfxSlider;
    private float audioVolume;
    private float sfxVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey(CONSTANT.SFXVolume) && PlayerPrefs.HasKey(CONSTANT.AudioVolume))
        {
            LoadVolume();
        }
        else
        {
            SetAudioVolume();
            SetSFXVolume();
        }
    }

    public void SetAudioVolume()
    {
        audioVolume = audioSlider.value;
        audioMixer.SetFloat(CONSTANT.Audio, Mathf.Log10(audioVolume) * 20);
    }

    public void SetSFXVolume()
    {
        sfxVolume = sfxSlider.value;
        audioMixer.SetFloat(CONSTANT.SFX, Mathf.Log10(sfxVolume) * 20);
    }

    private void LoadVolume()
    {
        audioSlider.value = PlayerPrefs.GetFloat(CONSTANT.AudioVolume);
        sfxSlider.value = PlayerPrefs.GetFloat(CONSTANT.SFXVolume);

        SetAudioVolume();
        SetSFXVolume();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(CONSTANT.AudioVolume, audioVolume);
        PlayerPrefs.SetFloat(CONSTANT.SFXVolume, sfxVolume);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ApplySavedVolume();
        }
    }
}
