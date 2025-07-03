using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Naruto_Iskill : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;

    [Header("I Skill")]
    [SerializeField] private float iForce;
    [SerializeField] private float moveTime;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
    }

    private void ActiveNarutoIskill()
    {
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * iForce, rb.velocity.y);
    }

}
