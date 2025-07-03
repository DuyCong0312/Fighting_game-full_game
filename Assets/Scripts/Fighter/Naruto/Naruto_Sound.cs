using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_Sound : MonoBehaviour
{
    public AudioClip firstAttack;
    public AudioClip thirdAttack;
    public AudioClip thirdAttack2;
    public AudioClip rangedAttack;
    public AudioClip jumpRangedAttack;
    public AudioClip specialAttack01;
    public AudioClip specialAttack02;
    public AudioClip wiSkill;
    public AudioClip sjSkill01;
    public AudioClip sjSkill02;
    public AudioClip winSound;

    private void PlaySoundFirstAttack()
    {
        AudioManager.Instance.PlaySFX(firstAttack);
    }

    private void PlaySoundThirdAttack()
    {
        AudioManager.Instance.PlaySFX(thirdAttack);
        AudioManager.Instance.PlaySFX(thirdAttack2);
    }

    private void PlaySoundRangedAttack()
    {
        AudioManager.Instance.PlaySFX(rangedAttack);
    }

    private void PlaySoundJumpRangedAttack()
    {
        AudioManager.Instance.PlaySFX(jumpRangedAttack);
        AudioManager.Instance.PlaySFX(thirdAttack2);
    }

    private void PlaySoundSpecialAttack01()
    {
        AudioManager.Instance.PlaySFX(specialAttack01);
    }

    private void PlaySoundSpecialAttack02()
    {
        AudioManager.Instance.PlaySFX(specialAttack02);
    }

    private void PlaySoundWiSkill()
    {
        AudioManager.Instance.PlaySFX(wiSkill);
    }

    private void PlaySoundSjSkill01()
    {
        AudioManager.Instance.PlaySFX(sjSkill01);
    }

    private void PlaySoundSjSkill02()
    {
        AudioManager.Instance.PlaySFX(sjSkill02);
        AudioManager.Instance.PlaySFX(thirdAttack2);
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
