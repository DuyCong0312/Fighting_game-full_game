using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_UpSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private KnockBack knockBack;
    private PlayerState playerState;
    private Animator anim;

    [Header("W+J Skill")]
    [SerializeField] private float wjForce;

    [Header("W+U Skill")]
    [SerializeField] private float speed;
    private float originalGravity;

    [Header("W+I Skill")]
    [SerializeField] private GameObject wiSkillPrefab;
    [SerializeField] private GameObject wiSkillEffectPrefab;
    [SerializeField] private Transform wiSkillPos;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        knockBack = GetComponentInParent<KnockBack>();
        playerState = GetComponentInParent<PlayerState>();
        anim = GetComponent<Animator>();
        originalGravity = rb.gravityScale;
    }

    private void ActiveKakashiWJskill()
    {
        StartCoroutine(WJskillMove());
    }

    private IEnumerator WJskillMove()
    {
        float direction = playerState.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * wjForce, rb.velocity.y);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
    }

    private void ActiveKakashiWUskillP1()
    {
        StartCoroutine(WUskillEnuP1());
    }

    private IEnumerator WUskillEnuP1()
    {
        yield return null;
        Transform opponent = knockBack.opponentDirection;
        float direction = opponent.eulerAngles.y == 0f ? -1f : 1f;
        Vector2 behindPosition = new Vector2(opponent.position.x + direction * 0.5f, opponent.position.y);
        transform.parent.position = behindPosition;
        if (direction == -1f)
        {
            playerState.isFacingRight = true;
            transform.parent.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            playerState.isFacingRight = false;
            transform.parent.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void ActiveKakashiWUskillP2()
    {
        StartCoroutine(WUskillMoveP2());
    }

    private IEnumerator WUskillMoveP2()
    {
        rb.gravityScale = 0f;
        CalWUskillVeloc();
        anim.speed = 0f;
        yield return new WaitForSeconds(0.25f);
        anim.speed = 1f;
        rb.velocity = Vector2.zero;
        while (playerState.isUsingSkill)
        {
            yield return null;
        }
        rb.gravityScale = originalGravity;
    }

    private void CalWUskillVeloc()
    {
        Vector2 movement;
        int directionAngle = playerState.isFacingRight ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (75f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }

    private void ActiveKakashiWIskill()
    {
        Instantiate(wiSkillEffectPrefab, wiSkillPos.position, wiSkillPos.transform.rotation);
        GameObject wiSkill = Instantiate(wiSkillPrefab, new Vector2(wiSkillPos.position.x + 0.75f, wiSkillPos.position.y + 0.5f), wiSkillPos.transform.rotation);
        Projectile skillCheck = wiSkill.GetComponent<Projectile>();
        if (skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }
}
