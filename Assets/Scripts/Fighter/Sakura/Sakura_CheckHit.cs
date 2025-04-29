using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_CheckHit : CheckHit
{
    private KnockBack knockBack;

    [Header("Normal Attack")]
    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private Transform jumpAttackPos;
    [SerializeField] private Vector2 attackBoxSize;
    [SerializeField] private float attackRange;

    [Header("U skill")]
    [SerializeField] private Transform UKskillPos;
    [SerializeField] private Vector2 UKskillRange;

    [Header("I skill")]
    [SerializeField] private Transform IskillPos;
    [SerializeField] private float IskillRange;
    [SerializeField] private Transform IKskillPos;
    [SerializeField] private Vector2 IKskillRange;

    private void FirstAttack()
    {
        StraightAttack(meleeAttack01Pos, attackBoxSize, 0f, 5f, transform.right);
        
    }

    private void SecondAttack()
    {
        RoundAttack(meleeAttack02Pos, attackRange, 5f, transform.right);
    }

    private void ThirdAttack()
    {
        StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f, transform.right);
    }

    private void JumpAttack()
    {
        RoundAttack(jumpAttackPos, attackRange, 5f, transform.right);
    }

    private void UKskillAttack()
    {
        StraightAttack(UKskillPos, UKskillRange, 0f, 10f, new Vector2 (transform.right.x,1));
    }

    public void IskillAttack()
    {
        RoundAttack(IskillPos, IskillRange, 5f, Vector2.zero);
    }
    private void IKskillAttack()
    {
        StraightAttack(IKskillPos, IKskillRange, 0f, 20f, new Vector2(transform.right.x, 1));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        // normal Attack
        Gizmos.DrawWireCube(meleeAttack01Pos.position, attackBoxSize);
        Gizmos.DrawWireSphere(meleeAttack02Pos.position, attackRange);
        Gizmos.DrawWireCube(meleeAttack03Pos.position, attackBoxSize);
        Gizmos.DrawWireSphere(jumpAttackPos.position, attackRange);
        //U skill
        Gizmos.DrawWireCube(UKskillPos.position, UKskillRange);
        //I skill
        Gizmos.DrawWireSphere(IskillPos.position, IskillRange);
        Gizmos.DrawWireCube(IKskillPos.position, IKskillRange);
    }
}
