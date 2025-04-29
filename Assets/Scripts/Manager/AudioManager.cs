using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private float audioVolume;
    private float sfxVolume;

    [Header ("Sound Effect")]
    public AudioClip jump;
    public AudioClip dash; 
    public AudioClip touchGround;
    public AudioClip step1;
    public AudioClip step3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ApplySavedVolume();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplySavedVolume()
    {
        audioVolume = PlayerPrefs.GetFloat(CONSTANT.AudioVolume, 1f);
        sfxVolume = PlayerPrefs.GetFloat(CONSTANT.SFXVolume, 1f);
        musicAudioSource.volume = audioVolume;
        sfxAudioSource.volume = sfxVolume;
    }
    public void PlayMusic(AudioClip clip)
    {
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxAudioSource.PlayOneShot(sfxClip);
    }
}
