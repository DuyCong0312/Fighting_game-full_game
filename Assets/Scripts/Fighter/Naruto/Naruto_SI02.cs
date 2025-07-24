using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_SI02 : SkillCheckHitUseOverLap
{
    [SerializeField] private Transform siPos;
    [SerializeField] private float circleRadius;
    private Animator anim;
    private float scaleRadius;
    private int hitCount = 0;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        scaleRadius = circleRadius; 
        StartCoroutine(CheckHitEnum());
        StartCoroutine(SkillEnum());
    }

    private IEnumerator SkillEnum()
    {
        yield return null;
        while (this.transform.localScale.x < 2.5f)
        {
            this.transform.localScale += new Vector3(0.02f, 0.02f, 0f);
            yield return null;
        }
        this.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
        anim.SetTrigger("Continue");
    }

    private IEnumerator CheckHitEnum()
    {
        while (true)
        {
            Vector3 actualScale = transform.lossyScale;
            scaleRadius = circleRadius * Mathf.Max(actualScale.x, actualScale.y);

            RoundAttack(siPos, scaleRadius, 2f, Vector2.zero, KnockBack.KnockbackType.Linear);
            CallHitEffect(HitEffect.HitEffectType.NormalHit);
            if (hit)
            {
                hitCount++;
            }
            yield return new WaitForSeconds(0.2f); 
            if (hitCount >= 9)
            {
                break;
            }
        }
        RoundAttack(siPos, scaleRadius, 5f, new Vector2(owner.transform.right.x, owner.transform.up.y), KnockBack.KnockbackType.BlownUp);
        CallHitEffect(HitEffect.HitEffectType.NormalHit);
        CallHitStop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white; 
        float drawRadius = Application.isPlaying
        ? scaleRadius
        : circleRadius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);

        Gizmos.DrawWireSphere(siPos.position, drawRadius);
    }
}

