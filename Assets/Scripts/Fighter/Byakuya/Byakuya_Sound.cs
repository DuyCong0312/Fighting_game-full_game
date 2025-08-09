using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byakuya_Sound : MonoBehaviour
{
    public AudioClip firstAttack;
    public AudioClip thirdAttack;
    public AudioClip thirdAttack2;
    public AudioClip jumpAttack;
    public AudioClip jumpRangedAttack;
    public AudioClip rangedAttack;
    public AudioClip spawnRangedAttack;
    public AudioClip specialAttack;
    public AudioClip wjSkill;
    public AudioClip wuSkill;
    public AudioClip wuEffectSkill;
    public AudioClip wiSkill;
    public AudioClip wiSkillSound;
    public AudioClip sjSkill;
    public AudioClip suSkill;
    public AudioClip siSkill01;
    public AudioClip siSkill02;
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
    private void PlaySoundThirdAttack02()
    {
        AudioManager.Instance.PlaySFX(thirdAttack2);
    }

    private void PlaySoundJumpAttack()
    {
        AudioManager.Instance.PlaySFX(jumpAttack);
    }
    private void PlaySoundJumpRangedAttack()
    {
        AudioManager.Instance.PlaySFX(jumpRangedAttack);
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

    private void PlaySoundWjSkill()
    {
        AudioManager.Instance.PlaySFX(wjSkill);
    }

    private void PlaySoundWuSkill()
    {
        AudioManager.Instance.PlaySFX(wuSkill);
        AudioManager.Instance.PlaySFX(wuEffectSkill);
    }

    private void PlaySoundWiSkill()
    {
        AudioManager.Instance.PlaySFX(wiSkill);
    }

    private void PlaySoundWiEffectSkill()
    {
        AudioManager.Instance.PlaySFX(wiSkillSound);
    }

    private void PlaySoundSjSkill()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.dash);
        AudioManager.Instance.PlaySFX(sjSkill);
    }

    private void PlaySoundSuSkill()
    {
        AudioManager.Instance.PlaySFX(suSkill);
    }

    private void PlaySoundSiSkill01()
    {
        AudioManager.Instance.PlaySFX(siSkill01);
    }

    private void PlaySoundSiSkill02()
    {
        AudioManager.Instance.PlaySFX(siSkill02);
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
