using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Input")]
    public PlayerInputSO playerInput;

    private IPlayerState currentState;

    [Header("Components")]
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public SpecialEffect specialEffect;
    public PlayerState playerState;
    public PlayerRage playerRage;
    public CheckGround groundCheck;
    public SpawnEffectAfterImage effectAfterImage;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 11f;
    public bool canDoubleJump = false;
    public float dashPower = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 0.5f;
    public Vector2 dashPos;
    public Vector2 jumpPos;
    public int defaultLayer;
    public int dashLayer;
    public float originalGravity;

    private float dashCooldownTimer = 0f;

    [Header("Attack Setting")]
    public bool canAttack = true;
    public int attackNumber;
    private ComboAttack comboAttack;
    private bool hasInterrupted = false;

    [Header("Special Move")]
    public SpecialMoveSO defenseAttack;
    public SpecialMoveSO defenseRangedAttack;
    public SpecialMoveSO defenseSpecialAttack;
    public SpecialMoveSO upAttack;
    public SpecialMoveSO upRangedAttack;
    public SpecialMoveSO upSpecialAttack;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        comboAttack = GetComponentInChildren<ComboAttack>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        specialEffect = GetComponentInChildren<SpecialEffect>();
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
        playerRage = GetComponent<PlayerRage>();
        groundCheck = GetComponent<CheckGround>();
        effectAfterImage = GetComponent<SpawnEffectAfterImage>();

        defaultLayer = gameObject.layer;
        dashLayer = LayerMask.NameToLayer(CONSTANT.Dashing);
        originalGravity = rb.gravityScale;
        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        CalJumpDash(); 
        if (!GameManager.Instance.gameStart
            || GameManager.Instance.gameEnded)
        {
            return;
        }

        if (currentState != null)
            currentState.UpdateState();

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = newState;

        if (currentState != null)
            currentState.EnterState(); 
        Debug.Log(currentState.GetType().Name);
    }

    public bool CanDash()
    {
        return dashCooldownTimer <= 0f && !playerState.isAttacking && !playerState.isGettingHurt;
    }

    public void StartDashCooldown()
    {
        dashCooldownTimer = dashCooldown;
    }

    public void GetHurtWhenAttacking()
    {
        if (playerState.isGettingHurt && !hasInterrupted)
        {
            hasInterrupted = true;
            comboAttack.StopCombo();
        }

        if (!playerState.isGettingHurt && hasInterrupted)
        {
            hasInterrupted = false;
        }
    }

    private void CalJumpDash()
    {
        Vector2 jumpPosValue = new Vector2(spriteRenderer.bounds.center.x, spriteRenderer.bounds.min.y);
        dashPos = jumpPosValue;
        jumpPos = jumpPosValue;
    }
}
