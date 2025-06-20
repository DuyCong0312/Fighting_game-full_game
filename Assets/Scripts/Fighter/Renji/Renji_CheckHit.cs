using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renji_CheckHit : CheckHit
{
    [Header("Normal Attack")]
    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private Transform jumpAttackPos;
    [SerializeField] private Vector2 attackBoxSize;
    [SerializeField] private float attackRange;

    [Header("U Skill")]
    [SerializeField] private Transform uSkillAttackPos;
    [SerializeField] private Vector2 uSkillBoxSize;
    [SerializeField] private Transform uKskillAttackPos;
    [SerializeField] private Vector2 uKskillBoxSize;

    [Header("W+J")]
    [SerializeField] private Transform WJpos;
    [SerializeField] private Vector2 attackWJSize;

    [Header("W+U")]
    [SerializeField] private Transform WUpos;
    [SerializeField] private Vector2 attackWUSize;

    [Header("W+I")]
    [SerializeField] private Transform WIpos;
    [SerializeField] private Vector2 attackWISize;

    [Header("S+J")]
    [SerializeField] private Transform SJpos;
    [SerializeField] private Vector2 attackSJSize;

    [Header("S+I")]
    [SerializeField] private Transform SIpos;
    [SerializeField] private Vector2 attackSISize;

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
        RoundAttack(meleeAttack03Pos, attackRange, 3f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void JumpAttack()
    {
        RoundAttack(jumpAttackPos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void UskillAttack()
    {
        StraightAttack(uSkillAttackPos, uSkillBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void UKskillAttack()
    {
        StraightAttack(uKskillAttackPos, uKskillBoxSize, -45f, 5f, transform.right, KnockBack.KnockbackType.Linear);
    }

    private void WJattack()
    {
        StraightAttack(WJpos, attackWJSize, 0f, 2f, this.transform.up * 0.1f, KnockBack.KnockbackType.Linear);
    }

    private void WUattack()
    {
        StraightAttack(WUpos, attackWUSize, 0f, 3f, this.transform.right, KnockBack.KnockbackType.Linear);
    }

    private void WIattack()
    {
        StartCoroutine(WIattackEnu(9, 5, new Vector2(transform.right.x, transform.up.y)));
    }

    private IEnumerator WIattackEnu(int count, int delay, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            StraightAttack(WIpos, attackWISize, 0f, 20f, direction, KnockBack.KnockbackType.BlownUp);
            yield return WaitFor.Frames(delay);
        }
    }

    public static class WaitFor
    {
        public static IEnumerator Frames(int frameCount)
        {
            while (frameCount-- > 0)
                yield return null;
        }
    }

    private void SJattack()
    {
        StraightAttack(SJpos, attackSJSize, 0f, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
    }

    private void SIattack()
    {
        StartCoroutine(SIattackEnu(9, 5, this.transform.right));
    }

    private IEnumerator SIattackEnu(int count, int delay, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            StraightAttack(SIpos, attackSISize, 0f, 5f, direction, KnockBack.KnockbackType.Linear);
            yield return WaitFor.Frames(delay);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        // Normal Attack
        Gizmos.DrawWireSphere(meleeAttack01Pos.position, attackRange);
        Gizmos.DrawWireCube(meleeAttack02Pos.position, attackBoxSize);
        Gizmos.DrawWireSphere(meleeAttack03Pos.position, attackRange);
        Gizmos.DrawWireSphere(jumpAttackPos.position, attackRange);
        // U skill
        Gizmos.DrawWireCube(uSkillAttackPos.position, uSkillBoxSize);
        DrawRotatedWireCube(uKskillAttackPos.position, uKskillBoxSize, -45f);
        // W+J
        Gizmos.DrawWireCube(WJpos.position, attackWJSize);
        // W+U
        Gizmos.DrawWireCube(WUpos.position, attackWUSize);
        // W+I
        Gizmos.DrawWireCube(WIpos.position, attackWISize);
        // S+J
        Gizmos.DrawWireCube(SJpos.position, attackSJSize);
        // S+I
        Gizmos.DrawWireCube(SIpos.position, attackSISize);
    }
    private void DrawRotatedWireCube(Vector3 center, Vector2 size, float angle)
    {
        Matrix4x4 m = Matrix4x4.TRS(center, Quaternion.Euler(0, 0, angle), Vector3.one);
        Gizmos.matrix = m;
        Gizmos.DrawWireCube(Vector3.zero, size);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
