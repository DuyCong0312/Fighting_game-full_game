using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_CheckHit : CheckHit
{
    [Header("Normal Attack")]
    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private Transform jumpAttackPos;
    [SerializeField] private Vector2 attackBoxSize;
    [SerializeField] private float attackRange;

    [Header("U skill")]
    [SerializeField] private Transform UskillPos;
    [SerializeField] private float UskillRange;

    [Header("I skill")]
    [SerializeField] private Transform IskillPos;
    [SerializeField] private float IskillRange;
    [SerializeField] private GameObject IskillEffectPrefab;
    [SerializeField] private Transform IskillEffectPos;

    [Header("W+J")]
    [SerializeField] private Transform WJpos;
    [SerializeField] private Vector2 attackWJSize;

    [Header("W+U")]
    [SerializeField] private Transform WUposP1;
    [SerializeField] private Vector2 attackWUP1Size;
    [SerializeField] private Transform WUposP2;
    [SerializeField] private Vector2 attackWUP2Size;

    [Header("S+J")]
    [SerializeField] private Transform SJpos;
    [SerializeField] private Vector2 attackSJSize;

    private void FirstAttack()
    {
        RoundAttack(meleeAttack01Pos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void SecondAttackP1()
    {
        StraightAttack(meleeAttack02Pos, attackBoxSize, 0f, 2f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Linear);
    }
    private void SecondAttackP2()
    {
        StraightAttack(meleeAttack02Pos, attackBoxSize, 0f, 3f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Linear);
    }

    private void ThirdAttack()
    {
        StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
    }

    private void JumpAttack()
    {
        RoundAttack(jumpAttackPos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void UskillAttack()
    {
        RoundAttack(UskillPos, UskillRange, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
    }

    private void IskillAttack()
    {
        StartCoroutine(IattackEnu(5, 0.2f, Vector2.zero));
    }
    private IEnumerator IattackEnu(int count, float delay, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            RoundAttack(IskillPos, IskillRange, 5f, direction, KnockBack.KnockbackType.Linear);
            yield return new WaitForSeconds(delay);
            if (i == count - 1)
            {
                Instantiate(IskillEffectPrefab, IskillEffectPos.position, Quaternion.identity);
            }
        }
    }

    private void WJattack()
    {
        StraightAttack(WJpos, attackWJSize, 0f, 2f, this.transform.right, KnockBack.KnockbackType.Linear);
    }

    private void WUattackP1()
    {
        StraightAttack(WUposP1, attackWUP1Size, 0f, 2f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
    }

    private void WUattackP2()
    {
        StraightAttack(WUposP2, attackWUP2Size, 0f, 3f, this.transform.right, KnockBack.KnockbackType.Linear);
    }

    private void WUattackP3()
    {
        StraightAttack(WUposP2, attackWUP2Size, 0f, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
    }

    private void SJattack()
    {
        StraightAttack(SJpos, attackSJSize, 0f, 5f, this.transform.right, KnockBack.KnockbackType.Linear);
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
        Gizmos.DrawWireSphere(UskillPos.position, UskillRange);
        //I skill
        Gizmos.DrawWireSphere(IskillPos.position, IskillRange);
        // W+J
        Gizmos.DrawWireCube(WJpos.position, attackWJSize);
        // W+U
        Gizmos.DrawWireCube(WUposP1.position, attackWUP1Size);
        Gizmos.DrawWireCube(WUposP2.position, attackWUP2Size);
        // S+J
        Gizmos.DrawWireCube(SJpos.position, attackSJSize);
    }
}
