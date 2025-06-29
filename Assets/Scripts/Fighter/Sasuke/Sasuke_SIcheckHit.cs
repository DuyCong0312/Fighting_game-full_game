using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sasuke_SIcheckHit : SkillCheckHitUseOverLap
{
    [SerializeField] private Transform skillPos;
    [SerializeField] private Vector2 skillBoxSize;
    private bool check = false;

    private void StartCheckHit()
    {
        StartCoroutine(StartCheckEnum());
    }

    private IEnumerator StartCheckEnum()
    {
        check = true;
        yield return null;
        while (check)
        {
            StraightAttack(skillPos, skillBoxSize, 0f, 10f, new Vector2(owner.transform.right.x, owner.transform.up.y), KnockBack.KnockbackType.BlownUp);
            yield return new WaitForSeconds(0.1f);
            if (hit)
            {
                check = false;
                break;
            }
        }
    }

    private void StopCheckHit()
    {
        check = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(skillPos.position, skillBoxSize);
    }
}
