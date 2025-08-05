using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpInputState : IPlayerState
{
    private PlayerStateMachine player;

    public UpInputState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.playerState.isUpInputPress = true;
    }

    public void UpdateState()
    {
        if (!Input.GetKey(player.playerInput.specialMoveUpInput) || !player.groundCheck.isGround)
        {
            player.ChangeState(new IdleState(player));
            return;
        }

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

        if (Input.GetKeyDown(player.playerInput.attack) && player.groundCheck.isGround)
        {
            var move = player.upAttack;
            player.ChangeState(new SpecialMoveState(player, move));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.rangedAttack) && !player.playerState.isUsingSkill && player.groundCheck.isGround)
        {
            var move = player.upRangedAttack;
            player.ChangeState(new SpecialMoveState(player, move));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.specialAttack) && !player.playerState.isUsingSkill && player.groundCheck.isGround)
        {
            var move = player.upSpecialAttack;
            player.specialEffect.callWIEffect = true;
            player.ChangeState(new SpecialMoveState(player, move));
            return;
        }

    }

    public void ExitState()
    {
    }
}
