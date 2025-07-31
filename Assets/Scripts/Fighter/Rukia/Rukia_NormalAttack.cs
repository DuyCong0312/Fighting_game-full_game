using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rukia_NormalAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private float originalGravity;

    [Header("First Attack")]
    [SerializeField] private float firstAttackForce;

    [Header("Second Attack")]
    [SerializeField] private float secondAttackForce;

    [Header("Third Attack")]
    [SerializeField] private float thirdAttackForce;
    [SerializeField] private float moveTime;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        originalGravity = rb.gravityScale;
    }

    private IEnumerator AttackMoveEnum(float force, float delay)
    {
        rb.gravityScale = 0f;
        yield return null;
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * force, rb.velocity.y);
        yield return new WaitForSeconds(delay);
        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
    }

    private void ActiveRukiaFirstAttack()
    {
        StartCoroutine(AttackMoveEnum(firstAttackForce, moveTime));
    }

    private void ActiveRukiaSecondAttack()
    {
        StartCoroutine(AttackMoveEnum(secondAttackForce, moveTime));
    }

    private void ActiveRukiaThirdAttack()
    {
        StartCoroutine(AttackMoveEnum(thirdAttackForce, moveTime));
    }
}
