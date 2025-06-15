using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    private PlayerState playerState;
    //private PlayerCombat playerCombat;
    private PlayerStateMachine playerStateMachine;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        //playerCombat = GetComponentInParent<PlayerCombat>();
        playerState = GetComponentInParent<PlayerState>();
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
    }

    private void StartCombo()
    {
        //playerCombat.canAttack = true;
        playerStateMachine.canAttack = true;
        if (playerStateMachine.attackNumber < 3)
        {
            playerStateMachine.attackNumber++;
        }
    }

    public void StopCombo()
    {
        playerState.isAttacking = false;
        //playerCombat.canAttack = true;
        //playerCombat.attackNumber = 0;
        playerStateMachine.canAttack = true;
        playerStateMachine.attackNumber = 0;
        anim.ResetTrigger(CONSTANT.FirstAttack);
        anim.ResetTrigger(CONSTANT.SecondAttack);
        anim.ResetTrigger(CONSTANT.ThirdAttack);
    }

}
