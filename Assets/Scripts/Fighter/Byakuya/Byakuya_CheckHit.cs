using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byakuya_CheckHit : CheckHit
{
    [Header("Normal Attack")]
    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private Vector2 attackBoxSize;
    [SerializeField] private float attackRange;

    [Header("U Skill")]
    [SerializeField] private Transform uSkillAttackPos;
    [SerializeField] private Vector2 uSkillBoxSize;

    [Header("W+J")]
    [SerializeField] private Transform WJpos;
    [SerializeField] private Vector2 attackWJSize;

    [Header("W+U")]
    [SerializeField] private Transform WUpos;
    [SerializeField] private Vector2 attackWUSize;

    [Header("S+J")]
    [SerializeField] private Transform SJpos;
    [SerializeField] private Vector2 attackSJSize;

    [Header("S+U")]
    [SerializeField] private Transform SUpos;
    [SerializeField] private Vector2 attackSUSize;

    [Header("S+I")]
    [SerializeField] private Transform SIpos;
    [SerializeField] private Vector2 attackSISize;

    private void FirstAttack()
    {
        StraightAttack(meleeAttack01Pos, attackBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
    }

    private void SecondAttack()
    {
        RoundAttack(meleeAttack02Pos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
    }

    private void ThirdAttack()
    {
        StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
        CallHitStop();
    }

    private void UskillAttack()
    {
        StraightAttack(uSkillAttackPos, uSkillBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void WJattack()
    {
        StraightAttack(WJpos, attackWJSize, 0f, 2f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
        CallHitStop();
    }

    private void WUattack()
    {
        StartCoroutine(CheckHitWUskill(3, 0.25f, new Vector2(transform.right.x, transform.up.y * 0.2f)));
    }

    private IEnumerator CheckHitWUskill(int count, float delay, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            StraightAttack(WUpos, attackWUSize, 45f, 2f, direction, KnockBack.KnockbackType.Linear);
            CallHitEffect(HitEffect.HitEffectType.SlashHit);
            yield return new WaitForSeconds(delay);
            if (i == count - 1)
            {
                StraightAttack(WUpos, attackWUSize, 45f, 4f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
                CallHitEffect(HitEffect.HitEffectType.SlashHit);
            }
        }
    }

    private void SJattack()
    {
        StraightAttack(SJpos, attackSJSize, 0f, 5f, this.transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
        CallHitStop();
    }

    private void SUattack()
    {
        StraightAttack(SUpos, attackSUSize, 0f, 10f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void SIattack()
    {
        StraightAttack(SIpos, attackSISize, 0f, 30f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
        CallHitStop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        // Normal Attack
        Gizmos.DrawWireCube(meleeAttack01Pos.position, attackBoxSize);
        Gizmos.DrawWireSphere(meleeAttack02Pos.position, attackRange);
        Gizmos.DrawWireCube(meleeAttack03Pos.position, attackBoxSize);
        // U skill
        Gizmos.DrawWireCube(uSkillAttackPos.position, uSkillBoxSize);
        // W+J
        Gizmos.DrawWireCube(WJpos.position, attackWJSize);
        // W+U
        DrawRotatedWireCube(WUpos.position, attackWUSize, -45f);
        // S+J
        Gizmos.DrawWireCube(SJpos.position, attackSJSize);
        // S+U
        Gizmos.DrawWireCube(SUpos.position, attackSUSize);
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
