using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sasuke_UpSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;

    [Header("W+J Skill")]
    [SerializeField] private float wjForce;

    [Header("W+U Skill")]
    [SerializeField] private float speed;
    private float originalGravity;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>(); 
        originalGravity = rb.gravityScale;
    }

    private void ActiveSasukeWJskill()
    {
        StartCoroutine(WskillMoveEnum(wjForce, 0.1f));
    }

    private IEnumerator WskillMoveEnum(float force, float delay)
    {
        rb.gravityScale = 0f;
        yield return null;
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * force, rb.velocity.y);
        yield return new WaitForSeconds(delay);
        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
    }

    private void ActiveSasukeWUskill()
    {
        StartCoroutine(WUskillEnum());
    }

    private IEnumerator WUskillEnum()
    {
        rb.gravityScale = 0f;
        yield return null;
        CalVelocityWUskill();
        while (playerState.isUsingSkill)
        {
            yield return null;
        }
        rb.gravityScale = originalGravity;
    }

    private void CalVelocityWUskill()
    {
        Vector2 movement;
        int directionAngle = playerState.isFacingRight ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (45f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }
}
