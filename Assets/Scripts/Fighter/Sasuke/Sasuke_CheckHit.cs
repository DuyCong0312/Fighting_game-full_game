using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sasuke_CheckHit : CheckHit
{
    [Header("Normal Attack")]
    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private Transform jumpAttackPos;
    [SerializeField] private Vector2 attackBoxSize;
    [SerializeField] private float attackRange;

    [Header("U skill")]
    [SerializeField] private Transform UskillPos01;
    [SerializeField] private Transform UskillPos02;
    [SerializeField] private Vector2 UskillBoxSize;
    [SerializeField] private Transform KUskillPos;
    [SerializeField] private Vector2 KUskillBoxSize;

    [Header("I skill")]
    [SerializeField] private GameObject LightningEffect;
    [SerializeField] private Transform IskillPos;
    [SerializeField] private float IskillRange;

    [Header("W+J")]
    [SerializeField] private Transform WJ01pos;
    [SerializeField] private Vector2 attackWJ01Size;
    [SerializeField] private Transform WJ02pos;
    [SerializeField] private Vector2 attackWJ02Size;
    [SerializeField] private Transform WJ03pos;
    [SerializeField] private Vector2 attackWJ03Size;
    [SerializeField] private Transform WJ04pos;
    [SerializeField] private Vector2 attackWJ04Size;

    [Header("W+U")]
    [SerializeField] private Transform WU01pos;
    [SerializeField] private float attackWU01Range;
    [SerializeField] private Transform WU02pos;
    [SerializeField] private float attackWU02Range;

    [Header("W+I")]
    [SerializeField] private Transform WIpos;
    [SerializeField] private float attackWIRange;
    private bool check = false;

    [Header("S+J")]
    [SerializeField] private Transform SJpos;
    [SerializeField] private Vector2 attackSJSize;

    [Header("S+U")]
    [SerializeField] private Transform SUpos;
    [SerializeField] private Vector2 attackSUSize;

    [Header("S+I")]
    [SerializeField] private Transform SI01pos;
    [SerializeField] private float attackSI01Range;
    [SerializeField] private Transform SI02pos;
    [SerializeField] private float attackSI02Range;
    [SerializeField] private Transform SI03pos;
    [SerializeField] private float attackSI03Range;

    private void FirstAttack()
    {
        RoundAttack(meleeAttack01Pos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void SecondAttackP1()
    {
        StraightAttack(meleeAttack02Pos, attackBoxSize, 0f, 2f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void SecondAttackP2()
    {
        StraightAttack(meleeAttack02Pos, attackBoxSize, 0f, 3f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void ThirdAttack()
    {
        StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void JumpAttack()
    {
        RoundAttack(jumpAttackPos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void UskillAttackP1()
    {
        StraightAttack(UskillPos01, UskillBoxSize, 0f, 5f, new Vector2(transform.right.x * 0.5f, transform.up.y * 0.4f), KnockBack.KnockbackType.Linear);
    }

    private void UskillAttackP2()
    {
        StraightAttack(UskillPos02, UskillBoxSize, 0f, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
    }

    private void KUskillAttack()
    {
        StraightAttack(KUskillPos, KUskillBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void IskillAttack()
    {
        StartCoroutine(IattackEnu(5, 0.1f, Vector2.zero));
    }

    private IEnumerator IattackEnu(int count, float delay, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            RoundAttack(IskillPos, IskillRange, 2f, direction, KnockBack.KnockbackType.Linear);
            yield return new WaitForSeconds(delay);
            if (i == count - 1)
            {
                Instantiate(LightningEffect, hitPos, Quaternion.identity);
                RoundAttack(IskillPos, IskillRange, 10f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
            }
        }
    }

    private void WJ01attack()
    {
        StraightAttack(WJ01pos, attackWJ01Size, 0f, 2f, this.transform.right, KnockBack.KnockbackType.Linear);
    }

    private void WJ02attack()
    {
        StraightAttack(WJ02pos, attackWJ02Size, 0f, 2f, Vector2.zero, KnockBack.KnockbackType.Linear);
    }

    private void WJ03attack()
    {
        StraightAttack(WJ03pos, attackWJ03Size, 0f, 2f, Vector2.zero, KnockBack.KnockbackType.Linear);
    }

    private void WJ04attack()
    {
        StraightAttack(WJ04pos, attackWJ04Size, 0f, 4f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
        if (hit)
        {
            Instantiate(LightningEffect, hitPos, Quaternion.identity);
        }
    }

    private void WU01attack()
    {
        RoundAttack(WU01pos, attackWU01Range, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
    }

    private void WU02attack()
    {
        RoundAttack(WU02pos, attackWU02Range, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
    }

    private void WIattack()
    {
        StartCoroutine(WIenum());
    }

    private IEnumerator WIenum()
    {
        check = true;
        while (check)
        {
            RoundAttack(WIpos, attackWIRange, 30f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
            yield return new WaitForSeconds(0.1f);
            if (hit)
            {
                Instantiate(LightningEffect, hitPos, Quaternion.identity);
            }
        }
    }

    private void StopCheckWIattack()
    {
        check = false;
    }

    private void SJattack()
    {
        StraightAttack(SJpos, attackSJSize, 0f, 5f, this.transform.right, KnockBack.KnockbackType.Linear); 
        if (hit)
        {
            Instantiate(LightningEffect, hitPos, Quaternion.identity);
        }
    }

    private void SUattack()
    {
        StraightAttack(SUpos, attackSUSize, 0f, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc); 
        if (hit)
        {
            Instantiate(LightningEffect, hitPos, Quaternion.identity);
        }
    }

    private void SI01attack()
    {
        RoundAttack(SI01pos, attackSI01Range, 5f, this.transform.right, KnockBack.KnockbackType.Linear);
    }
    private void SI02attack()
    {
        RoundAttack(SI02pos, attackSI02Range, 5f, Vector2.zero, KnockBack.KnockbackType.Linear);
    }
    private void SI03attack()
    {
        RoundAttack(SI03pos, attackSI03Range, 5f, new Vector2(transform.right.x, transform.up.y * -0.1f), KnockBack.KnockbackType.Linear);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        // Normal Attack
        Gizmos.DrawWireSphere(meleeAttack01Pos.position, attackRange);
        Gizmos.DrawWireCube(meleeAttack02Pos.position, attackBoxSize);
        Gizmos.DrawWireCube(meleeAttack03Pos.position, attackBoxSize);
        Gizmos.DrawWireSphere(jumpAttackPos.position, attackRange);
        //U skill
        Gizmos.DrawWireCube(UskillPos01.position, UskillBoxSize);
        Gizmos.DrawWireCube(UskillPos02.position, UskillBoxSize);
        Gizmos.DrawWireCube(KUskillPos.position, KUskillBoxSize);
        //I skill
        Gizmos.DrawWireSphere(IskillPos.position, IskillRange);
        // W+J
        Gizmos.DrawWireCube(WJ01pos.position, attackWJ01Size);
        Gizmos.DrawWireCube(WJ02pos.position, attackWJ02Size);
        Gizmos.DrawWireCube(WJ03pos.position, attackWJ03Size);
        Gizmos.DrawWireCube(WJ04pos.position, attackWJ04Size);
        // W+U
        Gizmos.DrawWireSphere(WU01pos.position, attackWU01Range);
        Gizmos.DrawWireSphere(WU02pos.position, attackWU02Range);
        // W+I
        Gizmos.DrawWireSphere(WIpos.position, attackWIRange);
        // S+J
        Gizmos.DrawWireCube(SJpos.position, attackSJSize);
        // S+U
        Gizmos.DrawWireCube(SUpos.position, attackSUSize);
        // S+I
        Gizmos.DrawWireSphere(SI01pos.position, attackSI01Range);
        Gizmos.DrawWireSphere(SI02pos.position, attackSI02Range);
        Gizmos.DrawWireSphere(SI03pos.position, attackSI03Range);
    }
}
