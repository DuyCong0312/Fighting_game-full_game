using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Naruto_CheckHit : CheckHit
{
    private CheckGround groundCheck;

    [Header("Normal Attack")]
    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private float attack01Range;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Vector2 attack02BoxSize;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private float attack03Range;
    [SerializeField] private Transform meleeAttack04Pos;
    [SerializeField] private Vector2 attack04BoxSize;
    [SerializeField] private Transform jumpAttackPos;

    [Header("U skill")]
    [SerializeField] private Transform KUskillPos;
    [SerializeField] private Vector2 KUskillBoxSize;

    [Header("I skill")]
    [SerializeField] private GameObject RasenganEffect;
    [SerializeField] private Transform IskillPos;
    [SerializeField] private float IskillRange;

    [Header("W+J")]
    [SerializeField] private Transform WJ01pos;
    [SerializeField] private Vector2 attackWJ01Size;
    [SerializeField] private Transform WJ02pos;
    [SerializeField] private Vector2 attackWJ02Size;

    [Header("W+U")]
    [SerializeField] private Transform WU01pos;
    [SerializeField] private Vector2 attackWU01Size;
    [SerializeField] private Transform WU02pos;
    [SerializeField] private float attackWU02Range;

    [Header("S+J")]
    [SerializeField] private GameObject SJskillEffect;
    [SerializeField] private Transform SJpos;
    [SerializeField] private float attackSJRange;

    protected override void Start()
    {
        base.Start();
        groundCheck = GetComponentInParent<CheckGround>();
    }

    private void FirstAttack()
    {
        RoundAttack(meleeAttack01Pos, attack01Range, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void SecondAttack()
    {
        StraightAttack(meleeAttack02Pos, attack02BoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }


    private void ThirdAttack()
    {
        RoundAttack(meleeAttack03Pos, attack03Range,  5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void FourAttack()
    {
        StraightAttack(meleeAttack04Pos, attack04BoxSize, 0f, 5f, new Vector2(this.transform.right.x, this.transform.up.y), KnockBack.KnockbackType.Arc);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void JumpAttack()
    {
        RoundAttack(jumpAttackPos, attack03Range, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void KUskillAttack()
    {
        StartCoroutine(CheckDamageDuringKUAttack());
    }

    private IEnumerator CheckDamageDuringKUAttack()
    {
        while (!groundCheck.isGround)
        {
            StraightAttack(KUskillPos, KUskillBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
            if (hit)
            {
                CallHitEffect(HitEffect.HitEffectType.NormalHit);
                CallHitStop();
                break;
            }
            yield return null;
        }
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
            CallHitEffect(HitEffect.HitEffectType.NormalHit);
            yield return new WaitForSeconds(delay);
            if (i == count - 1)
            {
                Instantiate(RasenganEffect, new Vector2(hitPos.x, hitPos.y - 0.5f), Quaternion.identity);
                RoundAttack(IskillPos, IskillRange, 10f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
                CallHitEffect(HitEffect.HitEffectType.NormalHit);
                CallHitStop();
            }
        }
    }

    private void WJ01attack()
    {
        StraightAttack(WJ01pos, attackWJ01Size, 0f, 2f, this.transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void WJ02attack()
    {
        StraightAttack(WJ02pos, attackWJ02Size, 0f, 3f, this.transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void WU01attack()
    {
        StraightAttack(WU01pos, attackWU01Size, 0f, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
    }

    private void WU02attack()
    {
        RoundAttack(WU02pos, attackWU02Range, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void SJattack()
    {
        RoundAttack(SJpos, attackSJRange, 5f, this.transform.right, KnockBack.KnockbackType.Linear);
        Instantiate(SJskillEffect, SJpos.position, SJpos.rotation);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        // Normal Attack
        Gizmos.DrawWireSphere(meleeAttack01Pos.position, attack01Range);
        Gizmos.DrawWireCube(meleeAttack02Pos.position, attack02BoxSize);
        Gizmos.DrawWireSphere(meleeAttack03Pos.position, attack03Range);
        Gizmos.DrawWireCube(meleeAttack04Pos.position, attack04BoxSize);
        Gizmos.DrawWireSphere(jumpAttackPos.position, attack03Range);
        //U skill
        Gizmos.DrawWireCube(KUskillPos.position, KUskillBoxSize);
        //I skill
        Gizmos.DrawWireSphere(IskillPos.position, IskillRange);
        // W+J
        Gizmos.DrawWireCube(WJ01pos.position, attackWJ01Size);
        Gizmos.DrawWireCube(WJ02pos.position, attackWJ02Size);
        // W+U
        Gizmos.DrawWireCube(WU01pos.position, attackWU01Size);
        Gizmos.DrawWireSphere(WU02pos.position, attackWU02Range);
        // S+J
        Gizmos.DrawWireSphere(SJpos.position, attackSJRange);
    }
}
