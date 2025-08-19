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
        CallSPskillEffect();
        player.animator.Play(move.animationName);
        player.playerRage.CostRage(move.rageCost);
        player.playerState.isUsingSkill = true;
        player.playerState.isAttacking = false;
    }

    private void CallSPskillEffect()
    {
        if (player.specialEffect.callWIEffect)
        {
            player.specialEffect.SpecialEffectSpawn(SpecialEffect.SpecialEffectType.WIskillAttack);
        }
        else if (player.specialEffect.callSIEffect)
        {
            player.specialEffect.SpecialEffectSpawn(SpecialEffect.SpecialEffectType.SIskillAttack);
        }
    }

    public void UpdateState()
    {
        if (!player.playerState.isUsingSkill)
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
        player.specialEffect.callWIEffect = false;
        player.specialEffect.callSIEffect = false;
    }
}

