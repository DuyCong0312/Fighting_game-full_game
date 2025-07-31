using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : IPlayerState
{
    private PlayerStateMachine player;

    public DefendState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.playerState.isDefending = true;
        player.animator.SetBool(CONSTANT.isDefend, true);
        player.rb.velocity = Vector2.zero;
    }

    public void UpdateState()
    {
        if (!Input.GetKey(player.playerInput.defense) || !player.groundCheck.isGround)
        {
            player.animator.SetBool(CONSTANT.isDefend, false);
            player.ChangeState(new FallingState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.jump) && player.groundCheck.isFloor)
        {
            player.groundCheck.MoveDownThroughFloor();
            player.ChangeState(new FallingState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.attack) && player.groundCheck.isGround)
        {
            var move = player.defenseAttack;
            player.ChangeState(new SpecialMoveState(player, move));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.rangedAttack) && !player.playerState.isUsingSkill && player.groundCheck.isGround)
        {
            var move = player.defenseRangedAttack;
            player.ChangeState(new SpecialMoveState(player, move));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.specialAttack) && !player.playerState.isUsingSkill && player.groundCheck.isGround)
        {
            var move = player.defenseSpecialAttack;
            player.ChangeState(new SpecialMoveState(player, move));
            return;
        }
    }

    public void ExitState()
    {
        player.animator.SetBool(CONSTANT.isDefend, false);
        player.playerState.isDefending = false;
    }
}
