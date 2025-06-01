using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rukia_Sound : MonoBehaviour
{
    public AudioClip firstAttack;
    public AudioClip thirdAttack;
    private void PlaySoundFirstAttack()
    {
        AudioManager.Instance.PlaySFX(firstAttack);
    }

    private void PlaySoundThirdAttack()
    {
        AudioManager.Instance.PlaySFX(thirdAttack);
    }
}
