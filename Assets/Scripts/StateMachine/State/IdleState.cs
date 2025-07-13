using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IdleState : IPlayerState
{
    private PlayerStateMachine player;

    public IdleState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
        player.animator.SetInteger(CONSTANT.CurrentState, 0);
    }

    public void UpdateState()
    {
        if (Input.GetKey(player.playerInput.moveLeft) || Input.GetKey(player.playerInput.moveRight))
        {
            player.ChangeState(new RunningState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.jump) && player.groundCheck.isGround)
        {
            player.ChangeState(new JumpingState(player));
            player.canDoubleJump = true;
            return;
        }

        if (Input.GetKeyDown(player.playerInput.attack) && player.canAttack)
        {
            player.ChangeState(new AttackState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.dash) && player.CanDash())
        {
            player.ChangeState(new DashState(player));
            return;
        }

        if (Input.GetKey(player.playerInput.defense) && player.groundCheck.isGround)
        {
            player.ChangeState(new DefendState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.rangedAttack) && !player.playerState.isUsingSkill)
        {
            player.ChangeState(new RangedAttackState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.specialAttack) && !player.playerState.isUsingSkill)
        {
            player.ChangeState(new SpecialAttackState(player));
            return;
        }

        if (Input.GetKey(player.playerInput.specialMoveUpInput))
        {
            player.ChangeState(new UpInputState(player));
            return;
        }
    }

    public void ExitState()
    {
        
    }
}
