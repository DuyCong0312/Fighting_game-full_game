using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_CheckHit : CheckHit
{
    [Header("Normal Attack")]
    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private Transform jumpAttackPos;
    [SerializeField] private Vector2 attackBoxSize;
    [SerializeField] private float attackRange;

    [Header("W+J")]
    [SerializeField] private Transform WJpos;
    [SerializeField] private Vector2 attackWJSize;

    [Header("S+U")]
    [SerializeField] private Transform SUpos;
    [SerializeField] private Vector2 attackSUSize;

    private void FirstAttack()
    {
        RoundAttack(meleeAttack01Pos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void SecondAttack()
    {
        StraightAttack(meleeAttack02Pos, attackBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void ThirdAttack()
    {
        StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f, new Vector2(transform.right.x,transform.up.y), KnockBack.KnockbackType.Arc);
    }

    private void JumpAttack()
    {
        RoundAttack(jumpAttackPos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void WJattack()
    {
        StraightAttack(WJpos, attackWJSize, 0f, 2f, new Vector2(transform.right.x, 1), KnockBack.KnockbackType.Arc);
    }

    private void SUattack()
    {
        StraightAttack(SUpos, attackSUSize, 0f, 5f, new Vector2(transform.right.x, 1), KnockBack.KnockbackType.Arc);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        // Normal Attack
        Gizmos.DrawWireSphere(meleeAttack01Pos.position, attackRange);
        Gizmos.DrawWireCube(meleeAttack02Pos.position, attackBoxSize);
        Gizmos.DrawWireCube(meleeAttack03Pos.position, attackBoxSize);
        Gizmos.DrawWireSphere(jumpAttackPos.position, attackRange);
        // W+J
        Gizmos.DrawWireCube(WJpos.position, attackWJSize);
        //S+U
        Gizmos.DrawWireCube(SUpos.position, attackSUSize);
    }
}
