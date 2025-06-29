using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sasuke_DefenseSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("S+J Skill")]
    [SerializeField] private float sjForce;

    [Header("S+U Skill")]
    [SerializeField] private float suForce;

    [Header("S+I Skill")]
    [SerializeField] private GameObject siSkillPrefab;
    [SerializeField] private Transform siSkillPos;
    [SerializeField] private float siForce;
    private float originalGravity;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
        originalGravity = rb.gravityScale;
    }

    private void ActiveSasukeSJskill()
    {
        StartCoroutine(SskillMoveEnum(sjForce, 0.1f));
    }

    private void ActiveSasukeSUskill()
    {
        StartCoroutine(SskillMoveEnum(suForce, 0.1f));
    }

    private IEnumerator SskillMoveEnum(float force, float delay)
    {
        rb.gravityScale = 0f;
        yield return null;
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * force, rb.velocity.y);
        yield return new WaitForSeconds(delay);
        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
    }

    private void ActiveSasukeSIskillMove()
    {
        StartCoroutine(SIskillEnum());
    }

    private IEnumerator SIskillEnum()
    {
        effectAfterImage.StartAfterImageEffect();
        rb.gravityScale = 0f;
        yield return null;
        CalVelocitySIskill();
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        effectAfterImage.StopAfterImageEffect();
        while (playerState.isUsingSkill)
        {
            yield return null;
        }
        rb.gravityScale = originalGravity;
    }

    private void CalVelocitySIskill()
    {
        Vector2 movement;
        int directionAngle = playerState.isFacingRight ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (45f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * siForce;
    }

    private void ActiveSasukeSIskill()
    {
        GameObject skill = Instantiate(siSkillPrefab, siSkillPos.position, siSkillPos.rotation);
        SkillCheckHitUseOverLap skillCheck = skill.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck != null) 
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }
}
