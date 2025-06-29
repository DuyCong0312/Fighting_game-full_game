using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sasuke_Iskill : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("I Skill")]
    [SerializeField] private float force;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
    }

    private void ActiveSasukeISkill()
    {
        effectAfterImage.StartAfterImageEffect();
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * force, rb.velocity.y);
    }
}
