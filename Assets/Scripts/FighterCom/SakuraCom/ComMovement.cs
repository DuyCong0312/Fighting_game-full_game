using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class ComMovement : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private CheckGround groundCheck;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;
    [SerializeField] private Transform player;

    [Header("Jump Setting")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 11f;
    public bool isFalling = false;

    private Vector2 jumpPos;
    private Vector2 dashPos;

    [Header("Dash Setting")]
    [SerializeField] private float dashPower = 10f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private bool canDash = true;
    private int defaultLayer;
    private int dashLayer;
    public bool isDashing;

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
        playerState.isFacingRight = false;
    }

    void Update()
    {
        UpdateAnimation();
        CalJumpDash();
        if (!GameManager.Instance.gameStart
            || GameManager.Instance.gameEnded
            || playerState.isAttacking
            || playerState.isUsingSkill
            || playerState.isDefending
            || playerState.isGettingHurt)
        {
            return;
        }
        Flipped();
    }

    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        anim.SetBool(CONSTANT.Running, true);
    }

    public void StopMoveToPlayer()
    {
        anim.SetBool(CONSTANT.Running, false);
    }
    private void Flipped()
    {
        if (player.position.x > this.transform.position.x && playerState.isFacingRight == false)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            playerState.isFacingRight = true;
        }
        else if (player.position.x < this.transform.position.x && playerState.isFacingRight == true)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            playerState.isFacingRight = false;
        }
    }

    public void HandleDash()
    {
        if (canDash)
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
        rb.velocity = Vector2.zero;
        rb.gravityScale = originalGravity;
        SetLayerRecursively(gameObject, defaultLayer);
        anim.SetBool(CONSTANT.isDashing, false);
        effectAfterImage.StopAfterImageEffect();
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void HandleJump()
    {
        if(groundCheck.isGround == true)
        {
            Jump();
            groundCheck.isJumping = true;
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool(CONSTANT.Jumping, true);
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.jump, jumpPos, transform.rotation);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
    }

    private void UpdateAnimation()
    {
        if (anim.GetBool(CONSTANT.Jumping))
        {
            if (rb.velocity.y < 0.1f)
            {
                anim.SetBool(CONSTANT.Falling, true);
                isFalling = true;
                anim.SetBool(CONSTANT.Jumping, false);
            }
        }
        if (groundCheck.isGround && anim.GetBool(CONSTANT.Falling))
        {
            anim.SetBool(CONSTANT.Falling, false);
            isFalling = false;
            EffectManager.Instance.SpawnEffect(EffectManager.Instance.touchGround, jumpPos, transform.rotation);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.touchGround);
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
