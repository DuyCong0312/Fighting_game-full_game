using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakuraComLogicCombat : MonoBehaviour
{
    private enum ComAction { None, Combo, Movement, Combat, Skill}
    ComAction currentAction = ComAction.None;

    private ComMovement move;
    private ComAttack attack;
    private ComUseSkill skill;
    private PlayerState playerState;
    private PlayerHealth health;
    private PlayerRage rage;
    private CheckGround groundCheck;
    public int comboStep = 0;
    private bool isActionOnCooldown = false;
    public float actionCooldownTime = 0.2f;

    private void Start()
    {
        move = GetComponentInParent<ComMovement>();
        attack = GetComponentInParent<ComAttack>();
        skill = GetComponentInParent<ComUseSkill>(); 
        playerState = GetComponentInParent<PlayerState>();
        health = GetComponentInParent<PlayerHealth>();
        rage = GetComponentInParent<PlayerRage>(); 
        groundCheck = GetComponentInParent<CheckGround>();
    }

    private void Update()
    {
        if (!GameManager.Instance.gameStart
             || GameManager.Instance.gameEnded
             || playerState.isUsingSkill
             || playerState.isAttacking
             || playerState.isDefending
             || playerState.isGettingHurt)
        {
            return;
        }
        if (isActionOnCooldown)
        {
            return;
        }
        UtilityCom();
        ExecuteCurrentAction();
    }

    private void UtilityCom()
    {
        if (currentAction != ComAction.None) return;

        float comboScore = 0f;
        float moveScore = 0f;
        float combatScore = 0f;
        float skillScore = 0f;

        comboScore = EvaluateCombo();
        moveScore = EvaluateMovement();
        combatScore = EvaluateCombat();
        skillScore = EvaluateSkill();

        float maxScore = Mathf.Max(comboScore, moveScore, combatScore, skillScore);

        if (maxScore == comboScore)
            currentAction = ComAction.Combo;
        else if (maxScore == moveScore)
            currentAction = ComAction.Movement;
        else if (maxScore == combatScore)
            currentAction = ComAction.Combat;
        else
            currentAction = ComAction.Skill;
    }

    private float EvaluateCombo()
    {
        float score = Random.Range(0f, 30f);
        if (rage.currentRage < 35f) score += 20f;
        if (attack.GetDistanceX() > 6f) score += 20f;
        return score;
    }

    private float EvaluateMovement()
    {
        float score = Random.Range(0f, 30f);
        if (attack.GetDistanceX() >= 5f) score += 40f;
        return score;
    }

    private float EvaluateCombat()
    {
        float score = Random.Range(0f, 30f);
        if (health.currentHealth < 25f) score += 20f;
        else score += 30f;
        return score;
    }

    private float EvaluateSkill()
    {
        float score = Random.Range(0f, 30f);
        if (rage.currentRage > 35f) score += 30f;
        else score += 15f;
        if (!groundCheck.isGround) score += 30f;
        return score;
    }

    void ExecuteCurrentAction()
    {

        Debug.Log(currentAction);
        switch (currentAction)
        {
            case ComAction.Combo:
                FirstCombo();
                break;

            case ComAction.Movement:
                HandleMovement();
                break;

            case ComAction.Combat:
                HandleCombat();
                break;

            case ComAction.Skill:
                HandleSkill();
                break;
        }
        StartCoroutine(ActionCooldown(actionCooldownTime));
    }

    private void HandleMovement()
    {
        float distance = attack.GetDistanceX();
        bool IsPlayerAbove = attack.GetDistanceY() >= 0.25f;


        if (IsPlayerAbove)
        {
            move.HandleJump();
        }

        if (distance >= 4.5f)
        {
            move.HandleDash();
        }
        else if (distance >= 1f && !move.isDashing)
        {
            move.MoveToPlayer();
        }
        else
        {
            move.StopMoveToPlayer();
        }
        currentAction = ComAction.None;
    }

    private void HandleCombat()
    {
        float distance = attack.GetDistanceX();
        float randomCombat = Random.Range(0f, 40f);
        float combatScore = (health.currentHealth > 15f) ? 60f : 20f;
        combatScore += randomCombat;

        if (playerState.isDefending || playerState.isAttacking)
        {
            return;
        }

        if (combatScore >= 60f && distance <= 1f)
        {
            attack.Attack();
        }
        else 
        {
            attack.Defend();
        }
        currentAction = ComAction.None;
    }

    private void HandleSkill()
    {
        float distance = attack.GetDistanceX();

        if (playerState.isUsingSkill)
        {
            return;
        }

        if (rage.currentRage > 35f)
        {
            if (distance < 2f && groundCheck.isGround)
            {
                skill.PlayISkill();
            }
            else if (distance < 1f && !groundCheck.isGround)
            {
                skill.PlayISkill();
            }
        }
        else
        {
            if (distance > 2f)
            {
                skill.PlayUskill();
            }
        }
        if (!playerState.isUsingSkill)
        {
            currentAction = ComAction.None;
        }
    }
    private void FirstCombo()
    {
        bool isSkillEnd = playerState.isUsingSkill ? false : true;

        switch (comboStep)
        {
            case 0:
                if (comboStep == 0 && groundCheck.isGround)
                {
                    skill.PlayUskill();
                    comboStep++;
                }
                break;
            case 1:
                if (isSkillEnd)
                {
                    move.HandleDash();
                    comboStep++;
                }
                break;
            case 2:
                if (!move.isDashing)
                {
                    move.HandleJump();
                    comboStep++;
                }
                break;
            case 3:
                if (comboStep == 3)
                {
                    skill.PlayUskill();
                    comboStep = 0;
                    currentAction = ComAction.None;
                }
                break;
        }
    }
    private IEnumerator ActionCooldown(float delay)
    {
        isActionOnCooldown = true;
        yield return new WaitForSeconds(delay);
        isActionOnCooldown = false;
    }
}
