using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : IPlayerState
{
    private PlayerStateMachine player;

    public HurtState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.animator.SetTrigger(CONSTANT.getHurt);
    }

    public void UpdateState()
    {
        if (!player.playerState.isGettingHurt)
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

