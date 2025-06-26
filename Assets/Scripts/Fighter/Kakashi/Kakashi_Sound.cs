using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_Sound : MonoBehaviour
{
    public AudioClip firstAttack;
    public AudioClip thirdAttack;
    public AudioClip thirdAttack2;
    public AudioClip jumpRangedAttack;
    public AudioClip specialAttack;
    public AudioClip wjSkill;
    public AudioClip wuSkill;
    public AudioClip wiSkill;
    public AudioClip suSkill;

    private void PlaySoundFirstAttack()
    {
        AudioManager.Instance.PlaySFX(firstAttack);
    }

    private void PlaySoundThirdAttack()
    {
        AudioManager.Instance.PlaySFX(thirdAttack);
    }

    private void PlaySoundThirdAttack2()
    {
        AudioManager.Instance.PlaySFX(thirdAttack2);
    }

    private void PlaySoundJumpRangedAttack()
    {
        AudioManager.Instance.PlaySFX(jumpRangedAttack);
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
    }
    private void PlaySoundWiSkill()
    {
        AudioManager.Instance.PlaySFX(wiSkill);
    }

    private void PlaySoundSjSkillP1()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.dash);
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

