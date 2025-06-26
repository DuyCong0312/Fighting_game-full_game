using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_Iskill : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;
    private PlayerStateMachine player;

    [Header("I Skill")]
    [SerializeField] private float force;
    private float originalGravity;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
        player = GetComponentInParent<PlayerStateMachine>();
        originalGravity = rb.gravityScale;
    }


    private void ActiveKakashiISkill()
    {
        StartCoroutine(IskillMove());
    }

    private IEnumerator IskillMove()
    {
        float direction = playerState.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * force, rb.velocity.y);
        rb.gravityScale = 0;
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.groundDash, player.dashPos, Quaternion.Euler(0, 180, 0) * player.transform.rotation);
        effectAfterImage.StartAfterImageEffect();
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        rb.gravityScale = originalGravity;
        effectAfterImage.StopAfterImageEffect();
    }
}
