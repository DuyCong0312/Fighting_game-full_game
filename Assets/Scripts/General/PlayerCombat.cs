using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CheckGround groundCheck;
    private PlayerState playerState;
    private ComboAttack comboAttack;

    public bool canAttack = true;
    public int attackNumber;
    [SerializeField] private float attackMoveDuration = 0.1f;
    private bool hasInterrupted = false;

    void Start()
    {
        comboAttack = GetComponentInChildren<ComboAttack>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<CheckGround>();
        playerState = GetComponent<PlayerState>();
    }
    void Update()
    {
        GetHurtWhenAttacking();

        if (!GameManager.Instance.gameStart
            || GameManager.Instance.gameEnded
            || playerState.isUsingSkill
            || playerState.isGettingHurt)
        {
            return;
        }
        Attack();
        Defend();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && canAttack)
        {
            canAttack = false;
            playerState.isAttacking = true;
            if (groundCheck.isGround)
            {
                anim.SetTrigger(attackNumber + CONSTANT.Attack);
                StartCoroutine(MoveWhenAttack());
            }
            else
            {
                anim.Play(CONSTANT.airAttack);
            }
        }
    }

    private void Defend()
    {
        if (Input.GetKey(KeyCode.S) && !playerState.isDefending && groundCheck.isGround)
        {
            playerState.isDefending = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            playerState.isDefending = false;
        }
        anim.SetBool(CONSTANT.isDefend, playerState.isDefending);
    }

    private IEnumerator MoveWhenAttack()
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

}

