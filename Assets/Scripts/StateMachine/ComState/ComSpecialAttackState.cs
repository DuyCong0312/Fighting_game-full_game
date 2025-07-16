using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComSpecialAttackState : IPlayerState
{
    private ComStateMachine com;

    public ComSpecialAttackState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        if (com.playerRage.currentRage >= 30f)
        {
            com.playerState.isUsingSkill = true;
            PerformSkill();
        }
        else
        {
            BackState();
        }
    }

    private void PerformSkill()
    {
        if (com.groundCheck.isGround)
        {
            com.playerRage.CostRage(30f);
            
            com.animator.SetTrigger(CONSTANT.Iskill);
        }
        else
        {

            bool hasAnimation = false;
            foreach (AnimationClip clip in com.animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == CONSTANT.IKskill)
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
            com.playerRage.CostRage(30f);
            com.animator.Play(CONSTANT.IKskill);
        }
    }

    public void UpdateState()
    {
        if (!com.playerState.isUsingSkill)
        {
            BackState();
        }
    }

    private void BackState()
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

    public void ExitState()
    {
    }

}


