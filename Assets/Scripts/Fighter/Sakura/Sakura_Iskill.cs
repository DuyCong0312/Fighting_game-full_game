using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Iskill : MonoBehaviour
{
    [Header("I Skill")]
    [SerializeField] private Sakura_CheckHit sakuraCheckHit;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Transform hitIskillSpawnEffect;
    [SerializeField] private Transform newPos;
    [SerializeField] private float moveSpeed;

    protected Rigidbody2D rb;

    [Header("I+K Skill")]
    [SerializeField] private float force;
    [SerializeField] private GameObject effectIK1;
    [SerializeField] private GameObject effectIK2;
    [SerializeField] private Transform effectIK01Pos;
    [SerializeField] private Transform effectIK02Pos;

    private Vector3 newPosition;
    private bool canMove = true;
    private float originalGravity;
    private Coroutine moveCoroutine;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void ActiveMove()
    {
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        newPosition = newPos.position;
        while (Vector2.Distance(transform.parent.position, newPosition) > 0.01f && canMove)
        {
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, newPosition, moveSpeed * Time.deltaTime);
            if (sakuraCheckHit.hit)
            {
                EffectManager.Instance.SpawnEffectUseTransform(hitEffect, hitIskillSpawnEffect, this.transform.rotation);
                canMove = false;
                break;
            }

            yield return null;
        }
        StopMove();
    }

    private void StopMove()
    {
        rb.gravityScale = originalGravity;
        canMove = true;
        moveCoroutine = null;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(CONSTANT.MainCamera) || collision.collider.CompareTag(CONSTANT.MapBorder))
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                StopMove();
            }
        }
    }

    private void ActiveSakuraIKSkill()
    {
        this.transform.rotation = this.transform.rotation; 
        rb.velocity = new Vector2 (rb.velocity.x, - force);
    }

    private void ActiveEffectIK1()
    {
        SpawnSkillEffect(effectIK1, effectIK01Pos);
    }

    private void ActiveEffectIK2()
    {
        SpawnSkillEffect(effectIK2, effectIK02Pos);
    }
    private void SpawnSkillEffect(GameObject name, Transform nameTransform)
    {
        Instantiate(name, nameTransform.position, nameTransform.rotation, nameTransform);
    }
}
