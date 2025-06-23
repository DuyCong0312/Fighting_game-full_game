using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byakuya_WIcheckHit : SkillCheckHitUseOverLap
{
    [SerializeField] private Transform attackPos;
    [SerializeField] private Vector2 attackBox;

    private void CheckHit()
    {
        StartCoroutine(CheckHitEnu(10, 0.1f, this.transform.up * 0.15f));
    }

    private IEnumerator CheckHitEnu(int count, float delay, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            StraightAttack(attackPos, attackBox, 0f, 1.5f, direction, KnockBack.KnockbackType.Linear);
            yield return new WaitForSeconds(delay);
            if (i == count - 1)
            {
                StraightAttack(attackPos, attackBox, 0f, 5f, new Vector2(owner.transform.right.x, owner.transform.up.y), KnockBack.KnockbackType.BlownUp);
                //ownerAnim.speed = 1f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPos.position, attackBox);
    }

}
