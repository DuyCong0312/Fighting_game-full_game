using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicBackGround : MonoBehaviour
{
    [SerializeField] private AudioClip backGroundMusic;

    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(backGroundMusic);
        }
    }
}
