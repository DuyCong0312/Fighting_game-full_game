using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Sound : MonoBehaviour
{
    public AudioClip firstAttack;
    public AudioClip thirdAttack;
    public AudioClip rangedAttack;
    public AudioClip touchGround;
    public AudioClip ik1Skill;

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
    }

    private void PlaySoundTouchGround()
    {
        AudioManager.Instance.PlaySFX(touchGround);
    }

    private void PlaySoundIk1Skill() 
    {
        AudioManager.Instance.PlaySFX(ik1Skill);
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
