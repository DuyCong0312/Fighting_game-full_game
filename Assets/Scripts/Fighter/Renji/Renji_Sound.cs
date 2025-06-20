using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renji_Sound : MonoBehaviour
{
    public AudioClip firstAttack;
    public AudioClip thirdAttack;
    public AudioClip jumpAttack;
    public AudioClip rangedAttack;
    public AudioClip specialAttack;
    public AudioClip wjSkill;
    public AudioClip wjEffectSkill;
    public AudioClip wuSkill;
    public AudioClip sjSkill;
    public AudioClip suSkill;
    public AudioClip siSkill01;
    public AudioClip siSkill02;
    public AudioClip winSound;

    private void PlaySoundFirstAttack()
    {
        AudioManager.Instance.PlaySFX(firstAttack);
    }

    private void PlaySoundThirdAttack()
    {
        AudioManager.Instance.PlaySFX(thirdAttack);
    }

    private void PlaySoundJumpAttack()
    {
        AudioManager.Instance.PlaySFX(jumpAttack);
    }

    private void PlaySoundRangedAttack()
    {
        AudioManager.Instance.PlaySFX(rangedAttack);
    }

    private void PlaySoundSpecialAttack()
    {
        AudioManager.Instance.PlaySFX(specialAttack);
    }

    private void PlaySoundWJskill()
    {
        AudioManager.Instance.PlaySFX(wjSkill);
    }

    private void PlaySoundWJEffectskill()
    {
        AudioManager.Instance.PlaySFX(wjEffectSkill);
    }

    private void PlaySoundWUskill()
    {
        AudioManager.Instance.PlaySFX(wuSkill);
    }

    private void PlaySoundSJskill()
    {
        AudioManager.Instance.PlaySFX(sjSkill);
    }

    private void PlaySoundSUskill()
    {
        AudioManager.Instance.PlaySFX(suSkill);
    }

    private void PlaySoundSIskill01()
    {
        AudioManager.Instance.PlaySFX(siSkill01);
    }

    private void PlaySoundSIskill02()
    {
        AudioManager.Instance.PlaySFX(siSkill02);
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
