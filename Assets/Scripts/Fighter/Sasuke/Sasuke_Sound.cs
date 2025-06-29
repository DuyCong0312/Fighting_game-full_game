using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sasuke_Sound : MonoBehaviour
{
    public AudioClip firstAttack;
    public AudioClip firstAttack2;
    public AudioClip thirdAttack;
    public AudioClip thirdAttack2;
    public AudioClip jumpRangedAttack;
    public AudioClip jumpRangedAttack02;
    public AudioClip rangedAttack;
    public AudioClip specialAttack;
    public AudioClip wiSkill;
    public AudioClip siSkill;
    public AudioClip winSound;

    private void PlaySoundFirstAttack()
    {
        AudioManager.Instance.PlaySFX(firstAttack);
        AudioManager.Instance.PlaySFX(firstAttack2);
    }

    private void PlaySoundThirdAttack()
    {
        AudioManager.Instance.PlaySFX(thirdAttack);
        AudioManager.Instance.PlaySFX(thirdAttack2);
    }

    private void PlaySoundJumpRangedAttackP1()
    {
        AudioManager.Instance.PlaySFX(jumpRangedAttack);
    }

    private void PlaySoundJumpRangedAttackP2()
    {
        AudioManager.Instance.PlaySFX(jumpRangedAttack02);
    }

    private void PlaySoundRangedAttack()
    {
        AudioManager.Instance.PlaySFX(rangedAttack);
    }

    private void PlaySoundSpecialAttack()
    {
        AudioManager.Instance.PlaySFX(specialAttack);
    }

    private void PlaySoundWiSkill()
    {
        AudioManager.Instance.PlaySFX(wiSkill);
    }

    private void PlaySoundSiSkill()
    {
        AudioManager.Instance.PlaySFX(siSkill);
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

