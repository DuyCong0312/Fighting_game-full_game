using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rukia_CheckHit : CheckHit
{
    [Header("Normal Attack")]
    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private Transform jumpAttackPos;
    [SerializeField] private Vector2 attackBoxSize;

    [Header("U skill")]
    [SerializeField] private Transform UskillPos;
    [SerializeField] private Vector2 UskillBoxSize;
    [SerializeField] private Transform UKskillPos;
    [SerializeField] private Vector2 UKskillBoxSize;

    [Header("I skill")]
    [SerializeField] private Transform IskillPos;
    [SerializeField] private Transform IKskillPos;
    [SerializeField] private Vector2 IskillBoxSize;

    [Header("W+J")]
    [SerializeField] private Transform WJpos;
    [SerializeField] private Vector2 WJBoxSize;

    [Header("W+U")]
    [SerializeField] private Transform WUpos;
    [SerializeField] private Vector2 WUBoxSize;

    [Header("S+J")]
    [SerializeField] private Transform SJpos;
    [SerializeField] private float SJrange;

    [Header("S+U")]
    [SerializeField] private Transform SUpos;
    [SerializeField] private float SUrange;

    [Header("S+I")]
    [SerializeField] private Transform SIpos;
    [SerializeField] private Vector2 SIBoxSize;

    private void FirstAttack()
    {
        StraightAttack(meleeAttack01Pos, attackBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
    }

    private void SecondAttack()
    {
        StraightAttack(meleeAttack02Pos, attackBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
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
        StartCoroutine(CheckDamageDuringJumpAttack());
    }

    private IEnumerator CheckDamageDuringJumpAttack()
    {
        while (playerState.isAttacking)
        {
            StraightAttack(jumpAttackPos, attackBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Linear);
            if (hit)
            {
                CallHitEffect(HitEffect.HitEffectType.NormalHit);
                break;
            }
            yield return null;
        }
    }

    private void UskillAttack()
    {
        StraightAttack(UskillPos, UskillBoxSize, 0f, 20f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void UKskillAttack()
    {
        StraightAttack(UKskillPos, UKskillBoxSize, -45f, 10f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void IskillAttack()
    {
        StraightAttack(IskillPos, IskillBoxSize, 0f, 20f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void IKskillAttack()
    {
        StraightAttack(IKskillPos, IskillBoxSize, -45f, 20f, new Vector2(transform.right.x, transform.up.y), KnockBack.KnockbackType.BlownUp);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void WJattack()
    {
        StartCoroutine(CheckDamageDuringWJAttack());
    }

    private IEnumerator CheckDamageDuringWJAttack()
    {
        while (playerState.isUsingSkill)
        {
            StraightAttack(WJpos, WJBoxSize, 0f, 5f, transform.right, KnockBack.KnockbackType.Arc);
            if (hit)
            {
                CallHitEffect(HitEffect.HitEffectType.NormalHit);
                break;
            }
            yield return null; 
        }
    }

    private void WUattack()
    {
        StraightAttack(WUpos, WUBoxSize, 45f, 2f, transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void SJattack()
    {
        RoundAttack(SJpos, SJrange, 5f, this.transform.right, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void SUattack()
    {
        RoundAttack(SUpos, SUrange, 2f, Vector2.zero, KnockBack.KnockbackType.Linear);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        if (hit == true)
        {
            foreach(var enemy in hitEnemiesThisFrame)
            {
                Animator opponentAnim = enemy.GetComponent<Animator>();
                if(opponentAnim != null)
                {
                    StartCoroutine(FreezeAnim(opponentAnim, 1f));
                }
            }
        }
    }
    private IEnumerator FreezeAnim(Animator anim, float duration)
    {
        float originalSpeed = anim.speed;
        anim.speed = 0f;
        yield return new WaitForSeconds(duration);
        anim.speed = originalSpeed;
    }

    private void SIattack()
    {
        StartCoroutine(SIattackEnu(10, 0.25f, Vector2.zero));
    }

    private IEnumerator SIattackEnu(int count, float delay, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            StraightAttack(SIpos, SIBoxSize, 0f, 1f, direction, KnockBack.KnockbackType.Linear);
            CallHitEffect(HitEffect.HitEffectType.NormalHit);
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        // Normal Attack
        Gizmos.DrawWireCube(meleeAttack01Pos.position, attackBoxSize);
        Gizmos.DrawWireCube(meleeAttack02Pos.position, attackBoxSize);
        Gizmos.DrawWireCube(meleeAttack03Pos.position, attackBoxSize);
        Gizmos.DrawWireCube(jumpAttackPos.position, attackBoxSize);
        // U skill
        Gizmos.DrawWireCube(UskillPos.position, UskillBoxSize);
        DrawRotatedWireCube(UKskillPos.position, UKskillBoxSize, -45f);
        // I skill
        Gizmos.DrawWireCube(IskillPos.position, IskillBoxSize);
        DrawRotatedWireCube(IKskillPos.position, IskillBoxSize, -45f);
        // W+J
        Gizmos.DrawWireCube(WJpos.position, WJBoxSize);
        // W+U
        DrawRotatedWireCube(WUpos.position, WUBoxSize, 45f);
        // S+J
        Gizmos.DrawWireSphere(SJpos.position, SJrange);
        // S+U
        Gizmos.DrawWireSphere(SUpos.position, SUrange);
        // S+I
        Gizmos.DrawWireCube(SIpos.position, SIBoxSize);
    }
    private void DrawRotatedWireCube(Vector3 center, Vector2 size, float angle)
    {
        Matrix4x4 m = Matrix4x4.TRS(center, Quaternion.Euler(0, 0, angle), Vector3.one);
        Gizmos.matrix = m;
        Gizmos.DrawWireCube(Vector3.zero, size);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
