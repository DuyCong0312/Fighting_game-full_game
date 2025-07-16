using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ComIdleState : IPlayerState
{
    private ComStateMachine com;

    public ComIdleState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        com.rb.velocity = new Vector2(0, com.rb.velocity.y);
        com.animator.SetInteger(CONSTANT.CurrentState, 0);
    }

    public void UpdateState()
    {
        com.Flipped();
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
        bool IsPlayerAbove = com.GetDistanceY() >= 0.25f;

        if (IsPlayerAbove)
        {
            com.ChangeState(new ComJumpingState(com));
            com.canDoubleJump = true;
            return;
        }

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
        float randomCombat = Random.Range(0f, 40f);
        float combatScore = (com.playerHealth.currentHealth > 15f) ? 30f : 20f;

        if (combatScore >= 60f && distance <= 1f)
        {
            com.ChangeState(new ComAttackState(com));
            return;
        }
        else
        {
            com.ChangeState(new ComDefendState(com));
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