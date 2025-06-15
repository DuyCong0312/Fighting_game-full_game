using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpecialAttackState : IPlayerState
{
    private PlayerStateMachine player;

    public SpecialAttackState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        if (player.playerRage.currentRage >= 30f)
        {
            player.playerState.isUsingSkill = true;
            PerformSkill();
        }
        else
        {
            BackState();
        }
    }

    private void PerformSkill()
    {
        if (player.groundCheck.isGround)
        {
            player.playerRage.CostRage(30f);
            player.animator.SetTrigger(CONSTANT.Iskill);
        }
        else
        {

            bool hasAnimation = false;
            foreach (AnimationClip clip in player.animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == CONSTANT.IKskill)
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
            player.playerRage.CostRage(30f);
            player.animator.Play(CONSTANT.IKskill);
        }
    }

    public void UpdateState()
    {
        if (!player.playerState.isUsingSkill) 
        {
            BackState();
        }
    }

    private void BackState()
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

    public void ExitState()
    {

    }
}

