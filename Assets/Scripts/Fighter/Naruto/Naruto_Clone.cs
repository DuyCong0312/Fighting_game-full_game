using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Naruto_Clone : SkillCheckHitUseOverLap
{
    private Rigidbody2D rb;
    private KnockBack knockBack;
    private Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float angleDir;
    [SerializeField] private Transform attackPos;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private Vector2 knockBackDir;
    [SerializeField] private float distanceToOpponent = 2f;
    [SerializeField] private float distanceFromStart = 5f;
    [SerializeField] private bool useSlashHitEffect;
    private Vector3 initialPosition;
    private Transform opponentTransform;

    protected override void Start()
    {
        base.Start();
        knockBack = owner.GetComponentInParent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initialPosition = this.transform.position;
        opponentTransform = knockBack.opponentDirection;
    }

    private void ActiveCloneEnum()
    {
        StartCoroutine(CLoneEnum());
    }

    private IEnumerator CLoneEnum()
    {
        anim.speed = 0f;
        yield return new WaitForSeconds(0.2f);
        CalculateVelocity();
        while (true)
        {
            float distanceToOpponentValue = Vector2.Distance(this.transform.position, opponentTransform.position);
            float distanceFromStartValue = Vector2.Distance(this.transform.position, initialPosition);

            if (distanceToOpponentValue < distanceToOpponent || distanceFromStartValue >= distanceFromStart)
            {
                break;
            }
            yield return null;
        }
        anim.speed = 1f;
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
        StraightAttack(attackPos, attackSize, 0f, damage, knockBackDir, KnockBack.KnockbackType.Linear);
        if (useSlashHitEffect)
        {
            CallHitEffect(HitEffect.HitEffectType.SlashHit);
        }
        else
        {
            CallHitEffect(HitEffect.HitEffectType.NormalHit);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }

}
