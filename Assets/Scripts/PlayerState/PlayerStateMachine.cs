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
    public PlayerState playerState;
    public PlayerRage playerRage;
    public CheckGround groundCheck;
    public SpawnEffectAfterImage effectAfterImage;

    [Header("Movement Settings")]
    public float speed = 4f;
    public float jumpForce = 6f;
    public bool canDoubleJump = false;
    public float dashPower = 10f;
    public float dashTime = 0.1f;
    public float dashCooldown = 1f;
    public Vector2 dashPos;
    public Vector2 jumpPos;
    public int defaultLayer;
    public int dashLayer;

    private float dashCooldownTimer = 0f;

    [Header("Attack Setting")]
    public bool canAttack = true;
    public int attackNumber;
    [SerializeField] private float attackMoveDuration = 0.1f;

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
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerState = GetComponent<PlayerState>();
        playerRage = GetComponent<PlayerRage>();
        groundCheck = GetComponent<CheckGround>();
        effectAfterImage = GetComponent<SpawnEffectAfterImage>();

        defaultLayer = gameObject.layer;
        dashLayer = LayerMask.NameToLayer(CONSTANT.Dashing);

        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        CalJumpDash();
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

    public IEnumerator MoveWhenAttack()
    {
        float direction = playerState.isFacingRight ? 1 : -1;
        float timer = 0f;

        while (timer < attackMoveDuration)
        {
            rb.velocity = new Vector2(direction * attackNumber * 2f, rb.velocity.y);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void CalJumpDash()
    {
        Vector2 jumpPosValue = new Vector2(spriteRenderer.bounds.center.x, spriteRenderer.bounds.min.y);
        dashPos = jumpPosValue;
        jumpPos = jumpPosValue;
    }
}
