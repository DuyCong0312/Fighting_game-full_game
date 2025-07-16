using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComLogicCombat : MonoBehaviour
{
    public enum ComAction { None, Movement, Combat, Skill }
    public ComAction currentAction = ComAction.None;

    private PlayerHealth health;
    private PlayerRage rage;
    private CheckGround groundCheck;
    private ComStateMachine com;
    public bool isActionOnCooldown = false;
    [SerializeField] private float actionCooldownTime = 0.2f;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        rage = GetComponent<PlayerRage>();
        groundCheck = GetComponent<CheckGround>();
        com = GetComponent<ComStateMachine>();
    }

    public void UtilityCom()
    {
        float moveScore = 0f;
        float combatScore = 0f;
        float skillScore = 0f;

        moveScore = EvaluateMovement();
        combatScore = EvaluateCombat();
        skillScore = EvaluateSkill();

        float maxScore = Mathf.Max(moveScore, combatScore, skillScore);

        if (maxScore == moveScore)
            currentAction = ComAction.Movement;
        else if (maxScore == combatScore)
            currentAction = ComAction.Combat;
        else
            currentAction = ComAction.Skill;
    }

    private float EvaluateMovement()
    {
        float score = Random.Range(0f, 30f);
        if (com.GetDistanceX() >= 5f) score += 40f;
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

    public IEnumerator ActionCooldown()
    {
        isActionOnCooldown = true;
        yield return new WaitForSeconds(actionCooldownTime);
        isActionOnCooldown = false;
    }

}
