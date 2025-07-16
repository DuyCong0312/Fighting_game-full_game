using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ComJumpingState : IPlayerState
{
    private ComStateMachine com;

    public ComJumpingState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        com.rb.velocity = new Vector2(com.rb.velocity.x, com.jumpForce);
        com.animator.SetInteger(CONSTANT.CurrentState, 2);
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.jump, com.jumpPos, com.transform.rotation);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
    }

    public void UpdateState()
    {
        com.Flipped();
        com.transform.position = Vector2.MoveTowards(com.transform.position, com.knockBack.opponentDirection.position, com.speed * Time.deltaTime);
        if (com.rb.velocity.y < 0.1f)
        {
            com.ChangeState(new ComFallingState(com));
            return;
        }
        if (com.comLogic.isActionOnCooldown)
        {
            return;
        }
        if (com.comLogic.currentAction == ComLogicCombat.ComAction.None)
        {
            com.comLogic.UtilityCom();
        }
        switch (com.comLogic.currentAction)
        {
            case ComLogicCombat.ComAction.Movement:
                HandleMovement();
                break;
            case ComLogicCombat.ComAction.Combat:
                HandleCombat();
                break;
            case ComLogicCombat.ComAction.Skill:
                HandleSkill();
                break;
        }
        com.comLogic.currentAction = ComLogicCombat.ComAction.None;
        com.comLogic.StartCoroutine(com.comLogic.ActionCooldown());
    }

    private void HandleMovement()
    {
        float distance = com.GetDistanceX();

        if (distance >= 4.5f && com.CanDash())
        {
            com.ChangeState(new ComDashState(com));
            return;
        }
        else if (distance >= 1f)
        {
            com.ChangeState(new ComRunningState(com));
            return;
        }
        else
        {
            return;
        }
    }

    private void HandleCombat()
    {
        float distance = com.GetDistanceX();

        if (distance <= 1f)
        {
            com.ChangeState(new ComAttackState(com));
            return;
        }
    }

    private void HandleSkill()
    {
        float distance = com.GetDistanceX();

        if (com.playerRage.currentRage > 35f)
        {
            if (distance < 2f)
            {
                com.ChangeState(new ComSpecialAttackState(com));
                return;
            }
        }
        else
        {
            if (distance > 2f)
            {
                com.ChangeState(new ComRangedAttackState(com));
                return;
            }
        }
        com.comLogic.currentAction = ComLogicCombat.ComAction.None;
    }
    public void ExitState()
    {

    }
}


