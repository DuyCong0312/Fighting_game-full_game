using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class AttackState : IPlayerState
{
    private PlayerStateMachine player;

    public AttackState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.canAttack = false;
        player.playerState.isAttacking = true;
       
        PerformAttack();
    }

    private void PerformAttack()
    {
        if (player.groundCheck.isGround)
        {
            player.animator.SetTrigger(player.attackNumber + CONSTANT.Attack);
        }
        else
        {
            player.animator.Play(CONSTANT.airAttack);
        }
    }

    public void UpdateState()
    {
        if (Input.GetKeyDown(player.playerInput.attack) && player.canAttack)
        {
            player.ChangeState(new AttackState(player));
            return;
        }

        if (!player.playerState.isAttacking)
        {
            if (player.groundCheck.isGround)
            {
                player.ChangeState(new IdleState(player));
                return;
            }
            else
            {
                player.ChangeState(new FallingState(player));
                return;
            }
        }
    }

    public void ExitState()
    {
    }
}
