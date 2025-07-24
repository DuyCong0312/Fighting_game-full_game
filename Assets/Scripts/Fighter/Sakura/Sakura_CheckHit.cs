using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_CheckHit : CheckHit
{
    private CheckGround groundCheck;

    [Header("Normal Attack")]
    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private Transform jumpAttackPos;
    [SerializeField] private Vector2 attackBoxSize;
    [SerializeField] private float attackRange;

    [Header("U skill")]
    [SerializeField] private Transform UKskillPos;
    [SerializeField] private float UK01skillRange;
    [SerializeField] private Vector2 UK02skillRange;

    [Header("I skill")]
    [SerializeField] private Transform IskillPos;
    [SerializeField] private float IskillRange;
    [SerializeField] private GameObject IskillEffectPrefab;
    [SerializeField] private Transform IKskillPos;
    [SerializeField] private Vector2 IK01skillRange;
    [SerializeField] private Vector2 IK02skillRange;

    [Header("W+J")]
    [SerializeField] private Transform WJ1pos;
    [SerializeField] private Vector2 WJ1size;
    [SerializeField] private Transform WJ2pos;
    [SerializeField] private float WJ2range;

    [Header("S+U")]
    [SerializeField] private Transform SUpos;
    [SerializeField] private Vector2 SUsize;

    [Header("S+I")]
    [SerializeField] private Transform SIpos;
    [SerializeField] private Vector2 SIsize;
    [SerializeField] private GameObject SIeffect;

    protected override void Start()
    {
        base.Start();
        groundCheck = GetComponentInParent<CheckGround>();
    }
    private void FirstAttack()
    {
        StraightAttack(meleeAttack01Pos, attackBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void SecondAttack()
    {
        RoundAttack(meleeAttack02Pos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void ThirdAttack()
    {
        StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void JumpAttack()
    {
        RoundAttack(jumpAttackPos, attackRange, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void UKskillAttack()
    {
        StartCoroutine(CheckDamageDuringKUAttack());
    }

    private IEnumerator CheckDamageDuringKUAttack()
    {
        while (!groundCheck.isGround)
        {
            RoundAttack(UKskillPos, UK01skillRange, 2f, transform.right, KnockBack.KnockbackType.Linear);
            CallHitEffect(HitEffect.HitEffectType.NormalHit);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
        StraightAttack(UKskillPos, UK02skillRange, 0f, 10f, new Vector2(transform.right.x, 1), KnockBack.KnockbackType.Arc);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }


    private void IskillAttack()
    {
        StartCoroutine(IattackEnu(4, 0.2f, Vector2.zero));
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
                GameObject effect = Instantiate(IskillEffectPrefab, IskillPos.position, this.transform.rotation);
                BlownUpEffect effectCheck = effect.GetComponent<BlownUpEffect>();
                if (effectCheck != null)
                {
                    effectCheck.SetOwner(this.gameObject);
                }
            }
        }
    }

    private void IKskillAttack()
    {
        StartCoroutine(CheckDamageDuringKIAttack());
    }

    private IEnumerator CheckDamageDuringKIAttack()
    {
        while (!groundCheck.isGround)
        {
            StraightAttack(IKskillPos, IK01skillRange, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
            CallHitEffect(HitEffect.HitEffectType.NormalHit);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
        StraightAttack(IKskillPos, IK02skillRange, 0f, 20f, new Vector2(transform.right.x, 1), KnockBack.KnockbackType.Arc);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void WJ_1attack()
    {
        StraightAttack(WJ1pos, WJ1size, 0f, 2f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void WJ_2attack()
    {
        RoundAttack(WJ2pos, WJ2range, 4f, new Vector2(transform.right.x, 1f), KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void SUattack()
    {
        StraightAttack(SUpos, SUsize, 0f, 4f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.Arc);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void SIattack()
    {
        StartCoroutine(SIattackEnu(12, 0.2f, this.transform.up * 0.1f));
    }

    private IEnumerator SIattackEnu(int count, float delay, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            StraightAttack(SIpos, SIsize, 0f, 2f, direction, KnockBack.KnockbackType.Linear);
            CallHitEffect(HitEffect.HitEffectType.NormalHit);
            yield return new WaitForSeconds(delay);
            if (i == count - 1) 
            {
                GameObject effect = Instantiate(SIeffect, new Vector2(SIpos.position.x, SIpos.position.y - 0.5f), SIpos.rotation);
                BlownUpEffect effectCheck = effect.GetComponent<BlownUpEffect>();
                if (effectCheck != null)
                {
                    effectCheck.SetOwner(this.gameObject);
                }
            }
        }
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
        Gizmos.DrawWireSphere(UKskillPos.position, UK01skillRange);
        Gizmos.DrawWireCube(UKskillPos.position, UK02skillRange);
        //I skill
        Gizmos.DrawWireSphere(IskillPos.position, IskillRange);
        Gizmos.DrawWireCube(IKskillPos.position, IK01skillRange);
        Gizmos.DrawWireCube(IKskillPos.position, IK02skillRange);
        //W+J
        Gizmos.DrawWireCube(WJ1pos.position, WJ1size);
        Gizmos.DrawWireSphere(WJ2pos.position, WJ2range);
        //S+U
        Gizmos.DrawWireCube(SUpos.position, SUsize);
        //S+I
        Gizmos.DrawWireCube(SIpos.position, SIsize);
    }
}
