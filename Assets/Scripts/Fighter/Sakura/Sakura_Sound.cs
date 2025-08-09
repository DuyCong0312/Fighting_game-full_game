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
    public AudioClip sjSkill;
    public AudioClip siSkill;
    public AudioClip startSound;
    public AudioClip winSound;

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

    private void PlaySoundSjSkill()
    {
        AudioManager.Instance.PlaySFX(sjSkill);
    }

    private void PlaySoundSi1Skill()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.dash);
    }

    private void PlaySoundSi2Skill()
    {
        AudioManager.Instance.PlaySFX(siSkill);
    }
    private void PlaySoundStart()
    {
        AudioManager.Instance.PlaySFX(startSound);
    }

    private void PlaySoundWin()
    {
        AudioManager.Instance.PlaySFX(winSound);
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
