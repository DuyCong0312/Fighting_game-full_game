using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerState playerState;
    private PlayerStateMachine playerStateMachine;
    private ComStateMachine comStateMachine;
    [SerializeField] private int maxAttackNumber;
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerState = GetComponentInParent<PlayerState>();
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
        if (playerStateMachine == null)
        {
            comStateMachine = GetComponentInParent<ComStateMachine>();
        }
    }

    private void StartCombo()
    {
        if (playerStateMachine != null)
        {
            playerStateMachine.canAttack = true;
            if (playerStateMachine.attackNumber < maxAttackNumber)
            {
                playerStateMachine.attackNumber++;
            }
        }
        else if (comStateMachine != null)
        {
            comStateMachine.canAttack = true;
            if (comStateMachine.attackNumber < maxAttackNumber)
            {
                comStateMachine.attackNumber++;
            }
        }
    }

    public void StopCombo()
    {
        if (playerStateMachine != null)
        {
            playerStateMachine.canAttack = true;
            playerStateMachine.attackNumber = 0;
        }
        else if (comStateMachine != null)
        {
            comStateMachine.canAttack = true;
            comStateMachine.attackNumber = 0;
        }
        playerState.isAttacking = false;
        anim.ResetTrigger(CONSTANT.FirstAttack);
        anim.ResetTrigger(CONSTANT.SecondAttack);
        anim.ResetTrigger(CONSTANT.ThirdAttack);
    }

}
