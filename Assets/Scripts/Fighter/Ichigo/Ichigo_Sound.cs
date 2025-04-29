using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_Sound : MonoBehaviour
{
    public AudioClip firstAttack;
    public AudioClip thirdAttack;
    public AudioClip rangedAttack;
    public AudioClip spawnRangedAttack;
    public AudioClip specialAttack;

    private void PlaySoundFirstAttack()
    {
        AudioManager.Instance.PlaySFX(firstAttack);
    }

    private void PlaySoundThirdAttack()
    {
        AudioManager.Instance.PlaySFX(thirdAttack);
    }

    private void PlaySoundRangedAttack() 
    {
        AudioManager.Instance.PlaySFX(rangedAttack);
        AudioManager.Instance.PlaySFX(spawnRangedAttack);
    }

    private void PlaySoundSpecialAttack()
    {
        AudioManager.Instance.PlaySFX(specialAttack);
    }

    private void Footstep()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.step1);
    }
    private void Footstep3()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.step3);
    }
}
