using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sasuke_SIcheckHit : SkillCheckHitUseOverLap
{
    [SerializeField] private Transform skillPos;
    [SerializeField] private Vector2 skillBoxSize;
    [SerializeField] private GameObject hitEffect;
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
            StraightAttack(skillPos, skillBoxSize, -45f, 10f, new Vector2(owner.transform.right.x, owner.transform.up.y), KnockBack.KnockbackType.BlownUp);
            yield return new WaitForSeconds(0.1f);
            if (hit)
            {
                Instantiate(hitEffect, hitPos, Quaternion.identity);
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
        DrawRotatedWireCube(skillPos.position, skillBoxSize, -45f);
    }

    private void DrawRotatedWireCube(Vector3 center, Vector2 size, float angle)
    {
        Matrix4x4 m = Matrix4x4.TRS(center, Quaternion.Euler(0, 0, angle), Vector3.one);
        Gizmos.matrix = m;
        Gizmos.DrawWireCube(Vector3.zero, size);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
