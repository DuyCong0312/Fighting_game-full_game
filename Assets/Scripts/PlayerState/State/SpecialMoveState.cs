using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpecialMoveState : IPlayerState
{
    private PlayerStateMachine player;
    private SpecialMoveSO move;

    public SpecialMoveState(PlayerStateMachine player, SpecialMoveSO move)
    {
        this.player = player;
        this.move = move;
    }

    public void EnterState()
    {
        if (player.playerRage.currentRage < move.rageCost)
        {
            player.ChangeState(new IdleState(player));
            return;
        }
        PerformSkill();

    }

    private void PerformSkill()
    {
        bool hasAnimation = false;
        foreach (AnimationClip clip in player.animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == move.animationName)
            {
                hasAnimation = true;
                break;
            }
        }
        if (!hasAnimation)
        {
            player.playerState.isUsingSkill = false;
            return;
        }
        player.animator.Play(move.animationName);
        player.playerRage.CostRage(move.rageCost);
        player.playerState.isUsingSkill = true;
        player.playerState.isAttacking = false;
    }

    public void UpdateState()
    {
        if (!player.playerState.isUsingSkill)
        {
            player.ChangeState(new IdleState(player));
            return;
        }
    }

    public void ExitState()
    {

    }
}

