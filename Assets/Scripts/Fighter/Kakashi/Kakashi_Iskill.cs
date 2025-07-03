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

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
        player = GetComponentInParent<PlayerStateMachine>();
    }


    private void ActiveKakashiISkill()
    {
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * force, rb.velocity.y);
    }
}
