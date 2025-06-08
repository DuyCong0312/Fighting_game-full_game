using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rukia_Sound : MonoBehaviour
{
    public AudioClip firstAttack;
    public AudioClip thirdAttack;
    public AudioClip jumpAttack;
    public AudioClip rangedAttack;
    public AudioClip spawnRangedAttack;
    public AudioClip specialAttack01;
    public AudioClip specialAttack02;
    public AudioClip wjSkill;
    public AudioClip sjSkill;
    public AudioClip suSkill;
    public AudioClip suSkillEffect;
    public AudioClip siSkill;
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

    private void PlaySoundSpawnRangedAttack()
    {
        AudioManager.Instance.PlaySFX(spawnRangedAttack);
    }

    private void PlaySoundSpecialAttack01()
    {
        AudioManager.Instance.PlaySFX(specialAttack01);
    }

    private void PlaySoundSpecialAttack02()
    {
        AudioManager.Instance.PlaySFX(specialAttack02);
        AudioManager.Instance.PlaySFX(spawnRangedAttack);
    }

    private void PlaySoundWJskill()
    {
        AudioManager.Instance.PlaySFX(wjSkill);
    }

    private void PlaySoundSJskill()
    {
        AudioManager.Instance.PlaySFX(sjSkill);
    }

    private void PlaySoundSUskill()
    {
        AudioManager.Instance.PlaySFX(suSkill);
    }

    private void PlaySoundSUskillEffect()
    {
        AudioManager.Instance.PlaySFX(suSkillEffect);
    }

    private void PlaySoundSIskill()
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
