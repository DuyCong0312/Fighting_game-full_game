using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byakuya_IskillCheckHit : SkillCheckHitUseOverLap
{
    [SerializeField] private float attackRange;
    private void CheckHit()
    {
        StartCoroutine(CheckHitEnu(10, 0.2f, Vector2.zero));
    }

    private IEnumerator CheckHitEnu(int count, float delay, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            RoundAttack(this.transform, attackRange, 1.5f, direction, KnockBack.KnockbackType.Linear);
            yield return new WaitForSeconds(delay);
            if (i == count - 1)
            {
                RoundAttack(this.transform, attackRange, 5f, new Vector2(owner.transform.right.x, owner.transform.up.y), KnockBack.KnockbackType.BlownUp);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
