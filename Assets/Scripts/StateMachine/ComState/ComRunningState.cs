using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ComRunningState : IPlayerState
{
    private ComStateMachine com;

    public ComRunningState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        com.transform.position = Vector2.MoveTowards(com.transform.position, com.knockBack.opponentDirection.position, com.speed * Time.deltaTime);
        com.animator.SetInteger(CONSTANT.CurrentState, 1);
    }

    public void UpdateState()
    {
        com.Flipped();
        if(com.GetDistanceX() >= 1f)
        {
            com.ChangeState(new ComIdleState(com));
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
            return;
        }
    }

    private void HandleCombat()
    {
        float distance = com.GetDistanceX();
        float randomCombat = Random.Range(0f, 40f);
        float combatScore = (com.playerHealth.currentHealth > 15f) ?30f : 20f;
        combatScore += randomCombat;

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