using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class ComAttackState : IPlayerState
{
    private ComStateMachine com;

    public ComAttackState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        com.canAttack = false;
        com.playerState.isAttacking = true;
        com.Flipped();
        PerformAttack();
    }

    private void PerformAttack()
    {
        if (com.groundCheck.isGround)
        {
            com.animator.SetTrigger(com.attackNumber + CONSTANT.Attack);
        }
        else
        {
            com.animator.Play(CONSTANT.airAttack);
        }
    }

    public void UpdateState()
    {
        com.GetHurtWhenAttacking();
        if (com.GetDistanceX() < 1f && com.canAttack)
        {
            com.ChangeState(new ComAttackState(com));
            return;
        }

        if (com.GetDistanceX() > 1f)
        {
            if (com.groundCheck.isGround)
            {
                com.ChangeState(new ComIdleState(com));
                return;
            }
            else
            {
                com.ChangeState(new ComFallingState(com));
                return;
            }
        }
    }

    public void ExitState()
    {
    }

}

