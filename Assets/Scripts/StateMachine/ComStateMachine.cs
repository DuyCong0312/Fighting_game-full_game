using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ComStateMachine : MonoBehaviour
{
    private IPlayerState currentState;

    [Header("Components")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public SpecialEffect specialEffect;
    public Rigidbody2D rb;
    public KnockBack knockBack;
    public PlayerState playerState;
    public PlayerRage playerRage;
    public PlayerHealth playerHealth;
    public CheckGround groundCheck;
    public SpawnEffectAfterImage effectAfterImage;
    public ComLogicCombat comLogic;

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
    private float distanceX;
    private float distanceY;

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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
        comboAttack = GetComponentInChildren<ComboAttack>();
        specialEffect = GetComponentInChildren<SpecialEffect>();
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
        playerState = GetComponent<PlayerState>();
        playerRage = GetComponent<PlayerRage>();
        playerHealth = GetComponent<PlayerHealth>();
        groundCheck = GetComponent<CheckGround>();
        effectAfterImage = GetComponent<SpawnEffectAfterImage>();
        comLogic = GetComponent<ComLogicCombat>();

        defaultLayer = gameObject.layer;
        dashLayer = LayerMask.NameToLayer(CONSTANT.Dashing);
        originalGravity = rb.gravityScale;
        ChangeState(new ComIdleState(this));
    }

    private void Update()
    {
        CalJumpDash();
        DistanceToPlayer();
        if (!GameManager.Instance.gameStart
             || GameManager.Instance.gameEnded)
        {
            return;
        }
        GetHurtWhenAttacking();
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

    public void Flipped()
    {
        if (knockBack.opponentDirection.position.x > this.transform.position.x && playerState.isFacingRight == false)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            playerState.isFacingRight = true;
        }
        else if (knockBack.opponentDirection.position.x < this.transform.position.x && playerState.isFacingRight == true)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            playerState.isFacingRight = false;
        }
    }

    public bool CanDash()
    {
        return dashCooldownTimer <= 0f && !playerState.isAttacking && !playerState.isGettingHurt;
    }

    public void StartDashCooldown()
    {
        dashCooldownTimer = dashCooldown;
    }

    private void GetHurtWhenAttacking()
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

    private void DistanceToPlayer()
    {
        distanceX = Mathf.Abs(knockBack.opponentDirection.position.x - this.transform.position.x);
        distanceY = knockBack.opponentDirection.position.y - this.transform.position.y;
    }

    public float GetDistanceX()
    {
        return distanceX;
    }

    public float GetDistanceY()
    {
        return distanceY;
    }
}
