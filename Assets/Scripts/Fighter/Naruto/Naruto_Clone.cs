using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Naruto_Clone : SkillCheckHitUseOverLap
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float angleDir;
    [SerializeField] private float time;
    [SerializeField] private Transform attackPos;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private Vector2 knockBackDir;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CLoneEnum());
    }

    private IEnumerator CLoneEnum()
    {
        yield return new WaitForSeconds(0.1f);
        CalculateVelocity();
        yield return new WaitForSeconds(time);
        rb.velocity = Vector2.zero;
    }

    private void CalculateVelocity()
    {
        Vector2 movement;
        float yRotation = transform.rotation.eulerAngles.y;
        int directionAngle = yRotation == 0f ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (angleDir * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }

    private void ChecKHit()
    {
        StraightAttack(attackPos, attackSize, 0f, 5f, knockBackDir, KnockBack.KnockbackType.Linear);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }

}
