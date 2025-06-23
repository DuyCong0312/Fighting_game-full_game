using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byakuya_KJcheckHit : SkillCheckHitUseOverLap
{
    [SerializeField] private float attackRange;

    private void CheckHit()
    {
        RoundAttack(this.transform, attackRange, 5, owner.transform.right, KnockBack.KnockbackType.Linear);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
