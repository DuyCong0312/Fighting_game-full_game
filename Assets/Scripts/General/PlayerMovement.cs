using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum State { Idle, Running, Jumping, Falling }
    private State currentState;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private CheckGround groundCheck;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("Jump Setting")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private bool isDoubleJump;

    private Vector2 jumpPos;
    private Vector2 dashPos;

    [Header("Dash Setting")]
    [SerializeField] private float dashPower = 10f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 1f; 
    [SerializeField] private bool isDashing;
    [SerializeField] private bool canDash = true;
    private int defaultLayer;
    private int dashLayer;

    void Start()
    {
        defaultLayer = gameObject.layer;
        dashLayer = LayerMask.NameToLayer(CONSTANT.Dashing);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponentInChildren<Collider2D>();
        groundCheck = GetComponent<CheckGround>();
        playerState = GetComponent<PlayerState>();
        effectAfterImage = GetComponent<SpawnEffectAfterImage>();
    }
     
    void Update()
    {
        UpdateAnimation();
        UpdateState();
        CalJumpDash();
        if (!GameManager.Instance.gameStart
            || GameManager.Instance.gameEnded
            || playerState.isUsingSkill 
            || playerState.isDefending
            || playerState.isGettingHurt)
        {
            return;
        }
        Movement();
        HandleJump();
        HandleDash();
    }

    private void Movement()
    {
        if (isDashing)
        {
            return;
        }
        else if (playerState.isAttacking)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return;
        }

        float movement = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            if (playerState.isFacingRight) 
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                playerState.isFacingRight = false;
            }
            movement = -1f; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!playerState.isFacingRight)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                playerState.isFacingRight = true;
            }
            movement = 1f;
        }
        rb.velocity = new Vector2(speed * movement, rb.velocity.y);
    }

    private void HandleJump()
    {
        if (isDashing || playerState.isAttacking)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.K) && groundCheck.isGround == true)
        {
            Jump();
            groundCheck.isGround = false;
            isDoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.K) && isDoubleJump && currentState == State.Falling)
        {
            Jump();
            isDoubleJump = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        currentState = State.Jumping;
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.jump, jumpPos, transform.rotation);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
    }

    private void HandleDash()
    {
        if (playerState.isAttacking)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.N) && canDash)
        {
            StartCoroutine(Dash());
            AudioManager.Instance.PlaySFX(AudioManager.Instance.dash);
            if (groundCheck.isGround)
            {
                EffectManager.Instance.SpawnEffect(EffectManager.Instance.groundDash, dashPos, Quaternion.Euler(0, 180, 0) * transform.rotation);
            }
            else
            {
                EffectManager.Instance.SpawnEffect(EffectManager.Instance.airDash, dashPos, Quaternion.Euler(0, 180, 0) * transform.rotation);
            }
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        SetLayerRecursively(gameObject, dashLayer);
        float direction = playerState.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * dashPower, 0f);
        anim.SetBool(CONSTANT.isDashing, true);
        effectAfterImage.StartAfterImageEffect();
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        SetLayerRecursively(gameObject, defaultLayer);
        anim.SetBool(CONSTANT.isDashing, false);
        effectAfterImage.StopAfterImageEffect();
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void UpdateAnimation()
    {
        anim.SetInteger(CONSTANT.CurrentState, (int)currentState);
    }

    private void UpdateState()
    {
        if (currentState == State.Jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                currentState = State.Falling;
            }
        }
        else if (currentState == State.Falling)
        {
            if (groundCheck.isGround)
            {
                currentState = State.Idle;
                EffectManager.Instance.SpawnEffect(EffectManager.Instance.touchGround, jumpPos, transform.rotation);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.touchGround);
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 1f)
        {
            currentState = State.Running;
        }
        else
        {
            currentState = State.Idle;
        }
    }

    private void CalJumpDash()
    {
        Vector2 jumpPosValue = new Vector2(spriteRenderer.bounds.center.x, spriteRenderer.bounds.min.y);
        jumpPos = jumpPosValue;
        dashPos = jumpPosValue;
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
