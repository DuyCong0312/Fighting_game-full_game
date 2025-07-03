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
            RoundAttack(this.transform, circleRadius, 2f, Vector2.zero, KnockBack.KnockbackType.Linear);
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
