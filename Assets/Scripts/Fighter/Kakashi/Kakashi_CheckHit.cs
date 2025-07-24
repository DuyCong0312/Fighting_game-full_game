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
    [SerializeField] private GameObject uSkillEffect;

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
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void SecondAttackP1()
    {
        StraightAttack(meleeAttack02Pos, attackBoxSize, 0f, 2f, new Vector2(transform.right.x, transform.up.y * 0.2f), KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
    }
    private void SecondAttackP2()
    {
        StraightAttack(meleeAttack02Pos, attackBoxSize, 0f, 3f, new Vector2(transform.right.x, transform.up.y * 0.2f), KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void ThirdAttack()
    {
        StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void JumpAttack()
    {
        RoundAttack(jumpAttackPos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
    }

    private void UskillAttack()
    {
        RoundAttack(UskillPos, UskillRange, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
        if (hit)
        {
            Instantiate(uSkillEffect, hitPos, this.transform.rotation);
        }
        CallHitStop();
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
            CallHitEffect(HitEffect.HitEffectType.NormalHit);
            yield return new WaitForSeconds(delay);
            if (i == count - 1)
            {
                GameObject effect = Instantiate(IskillEffectPrefab, IskillEffectPos.position, Quaternion.identity);
                BlownUpEffect effectCheck = effect.GetComponent<BlownUpEffect>();
                if(effectCheck != null)
                {
                    effectCheck.SetOwner(this.gameObject);
                }
            }
        }
    }

    private void WJattack()
    {
        StraightAttack(WJpos, attackWJSize, 0f, 2f, this.transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void WUattackP1()
    {
        StraightAttack(WUposP1, attackWUP1Size, 0f, 2f, new Vector2(transform.right.x, transform.up.y * 1.25f), KnockBack.KnockbackType.Arc);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void WUattackP2()
    {
        StraightAttack(WUposP2, attackWUP2Size, 0f, 3f, Vector2.zero, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
    }

    private void WUattackP3()
    {
        StraightAttack(WUposP2, attackWUP2Size, 0f, 5f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void SJattack()
    {
        StraightAttack(SJpos, attackSJSize, 0f, 5f, this.transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.SlashHit);
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
