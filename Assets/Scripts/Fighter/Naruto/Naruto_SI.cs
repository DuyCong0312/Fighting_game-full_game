using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_SI : SkillCheckHitUseOverLap
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius;
    private bool isGround;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(CheckHitEnum());
    }

    private IEnumerator CheckHitEnum()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            RoundAttack(this.transform, circleRadius, 2f, new Vector2(this.transform.right.x, -this.transform.up.y * 0.2f), KnockBack.KnockbackType.Linear);
            CallHitEffect(HitEffect.HitEffectType.NormalHit);
            isGround = Physics2D.OverlapCircle(this.transform.position, circleRadius, groundLayer);
            yield return null;
            if (isGround)
            {
                yield return new WaitForSeconds(0.2f);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, circleRadius);
    }
}
