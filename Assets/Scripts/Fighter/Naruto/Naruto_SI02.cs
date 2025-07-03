using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_SI02 : SkillCheckHitUseOverLap
{
    [SerializeField] private float circleRadius;
    private int hitCount = 0;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(CheckHitEnum());
        StartCoroutine(SkillEnum());
    }

    private IEnumerator SkillEnum()
    {
        yield return null;
        while (this.transform.localScale.x < 3f)
        {
            this.transform.localScale += new Vector3(0.05f, 0.05f, 0f);
            yield return null;
        }
        this.transform.localScale = new Vector3(3f, 3f, 1f);
    }

    private IEnumerator CheckHitEnum()
    {
        while (true)
        {
            RoundAttack(this.transform, circleRadius, 1f, Vector2.zero, KnockBack.KnockbackType.Linear);
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
        RoundAttack(this.transform, circleRadius, 5f, new Vector2(owner.transform.right.x, owner.transform.up.y), KnockBack.KnockbackType.BlownUp);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, circleRadius);
    }
}

