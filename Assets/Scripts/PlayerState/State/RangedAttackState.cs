using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : IPlayerState
{
    private PlayerStateMachine player;

    public RangedAttackState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.playerState.isUsingSkill = true;
        PerformSkill();
    }

    private void PerformSkill()
    {
        if (player.groundCheck.isGround)
        {
            player.playerRage.GetRage(5f);
            player.animator.SetTrigger(CONSTANT.Uskill);
        }
        else
        {

            bool hasAnimation = false;
            foreach (AnimationClip clip in player.animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == CONSTANT.UKskill)
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
            player.playerRage.GetRage(5f);
            player.animator.Play(CONSTANT.UKskill);
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

    }
}
