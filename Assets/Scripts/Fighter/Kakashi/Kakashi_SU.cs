using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_SU : SkillCheckHitUseOverLap
{
    private KnockBack knockBack;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToOpponent = 2f;
    [SerializeField] private float distanceFromStart = 5f;
    [SerializeField] private Transform skillCheckHitPos;
    [SerializeField] private float skillCheckHitRange;
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

    private void ActiveMove()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        int direction = transform.rotation.eulerAngles.y == 0f ? 1 : -1;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        anim.speed = 0f; while (true)
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

    private void CheckHit()
    {
        RoundAttack(skillCheckHitPos, skillCheckHitRange, 5f, this.transform.right, KnockBack.KnockbackType.Linear);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(skillCheckHitPos.position, skillCheckHitRange);
    }
}
