using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerState playerState;
    private CheckGround groundCheck;
    private float distanceX;
    private float distanceY;
    [SerializeField] private Transform player;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
        groundCheck = GetComponent<CheckGround>(); 
    }

    private void Update()
    {
        DistanceToPlayer();
        if (distanceX > 1f)
        {
            StopAttack();
        }
    }

    public void Attack()
    {
        if (groundCheck.isGround)
        {
            playerState.isAttacking = true;
            anim.SetBool(CONSTANT.isAttack, true);
        }
        else
        {
            anim.Play(CONSTANT.airAttack);
        }
    }

    public void Defend()
    {
        StartCoroutine(DefendCoroutine());
    }

    public void StopAttack()
    {
        playerState.isAttacking = false;
        anim.SetBool(CONSTANT.isAttack, false);
    }

    private IEnumerator DefendCoroutine()
    {
        playerState.isDefending = true;
        anim.SetBool(CONSTANT.isDefend, true);
        yield return new WaitForSeconds(1f);
        playerState.isDefending = false;
        anim.SetBool(CONSTANT.isDefend, false);
    }

    private void DistanceToPlayer()
    {
        distanceX = Mathf.Abs(player.position.x - transform.position.x);
        distanceY = player.position.y - transform.position.x;
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
