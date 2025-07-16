using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComRangedAttackState : IPlayerState
{
    private ComStateMachine com;

    public ComRangedAttackState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        com.playerState.isUsingSkill = true;
        PerformSkill();
    }

    private void PerformSkill()
    {
        if (com.groundCheck.isGround)
        {
            com.playerRage.GetRage(5f);
            com.animator.SetTrigger(CONSTANT.Uskill);
        }
        else
        {
            bool hasAnimation = false;
            foreach (AnimationClip clip in com.animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == CONSTANT.UKskill)
                {
                    hasAnimation = true;
                    break;
                }
            }
            if (!hasAnimation)
            {
                com.rb.gravityScale = com.originalGravity;
                com.playerState.isUsingSkill = false;
                return;
            }
            com.playerRage.GetRage(5f);
            com.animator.Play(CONSTANT.UKskill);
        }
    }

    public void UpdateState()
    {
        if (!com.playerState.isUsingSkill)
        {
            if (com.groundCheck.isGround)
            {
                com.ChangeState(new ComIdleState(com));
                return;
            }
            else
            {
                com.rb.gravityScale = com.originalGravity;
                com.ChangeState(new ComFallingState(com));
                return;
            }
        }
    }

    public void ExitState()
    {
    }

}


