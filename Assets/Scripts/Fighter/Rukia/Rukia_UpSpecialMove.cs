using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rukia_UpSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;
    private Animator anim;
    private float originalGravity;
    [SerializeField] private float WJskillSpeed;
    [SerializeField] private float WJskillTime;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
        anim = GetComponent<Animator>();
        originalGravity = rb.gravityScale;
    }

    private void ActiveRukiaWJSkill()
    {
        StartCoroutine(WJSkillEnu());
    }

    private IEnumerator WJSkillEnu()
    {
        rb.gravityScale = 0f;
        effectAfterImage.StartAfterImageEffect();
        playerState.isUsingSkill = true;
        CalculateVelocity();
        yield return new WaitForSeconds(WJskillTime);
        rb.velocity = Vector2.zero;
        effectAfterImage.StopAfterImageEffect();
        playerState.isUsingSkill = false;
        rb.gravityScale = originalGravity;
        anim.SetInteger(CONSTANT.CurrentState,3);
    }

    private void CalculateVelocity()
    {
        Vector2 movement;
        float yRotation = transform.rotation.eulerAngles.y;
        int directionAngle = yRotation == 0f ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (50f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * WJskillSpeed;
    }
}
