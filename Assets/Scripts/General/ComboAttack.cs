using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    private PlayerState playerState;
    private PlayerCombat playerCombat;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerCombat = GetComponentInParent<PlayerCombat>();
        playerState = GetComponentInParent<PlayerState>();
    }

    private void StartCombo()
    {
        playerCombat.canAttack = true;
        if (playerCombat.attackNumber < 3)
        {
            playerCombat.attackNumber++;
        }
    }

    public void StopCombo()
    {
        playerState.isAttacking = false;
        playerCombat.canAttack = true;
        playerCombat.attackNumber = 0;
        anim.ResetTrigger(CONSTANT.FirstAttack);
        anim.ResetTrigger(CONSTANT.SecondAttack);
        anim.ResetTrigger(CONSTANT.ThirdAttack);
    }

}
