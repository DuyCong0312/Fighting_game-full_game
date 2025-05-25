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
    public AudioClip wjSkill;
    public AudioClip wiSkill;
    public AudioClip sjSkill;
    public AudioClip siSkill;
    public AudioClip winSound;
    public AudioClip loseSound;

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

    private void PlaySoundWjSkill()
    {
        AudioManager.Instance.PlaySFX(wjSkill);
    }

    private void PlaySoundWiSkill()
    {
        AudioManager.Instance.PlaySFX(specialAttack);
        AudioManager.Instance.PlaySFX(wiSkill);
    }

    private void PlaySoundSu1Skill()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.dash);
    }

    private void PlaySoundSjSkill()
    {
        AudioManager.Instance.PlaySFX(sjSkill);
    }

    private void PlaySoundSi1Skill()
    {
        AudioManager.Instance.PlaySFX(siSkill);
    }

    private void PlaySoundSi2Skill()
    {
        AudioManager.Instance.PlaySFX(wiSkill);
    }
    private void PlaySoundWin()
    {
        AudioManager.Instance.PlaySFX(winSound);
    }

    private void PlaySoundLose()
    {
        AudioManager.Instance.PlaySFX(loseSound);
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
