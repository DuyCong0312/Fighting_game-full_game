using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckHit : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemies;
    protected SpriteRenderer spriteRenderer;
    protected PlayerRage playerRage;
    protected Collider2D coll;
    public bool hit = false;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
        playerRage = GetComponentInParent<PlayerRage>();
        coll = GetComponent<Collider2D>();
    }

    protected void StraightAttack(Transform AttackPos, Vector2 AttackSize, float Angle, float AttackDamage, Vector2 KnockBackDirection, KnockBack.KnockbackType Type)
    {
        hit = false;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPos.position, AttackSize, Angle, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy == coll)
            {
                continue; 
            }
            hit = true;
            this.spriteRenderer.sortingOrder = 1;
            this.playerRage.GetRage(5f);
            enemy.GetComponentInParent<PlayerHealth>().TakeDamage(AttackDamage, KnockBackDirection, Type);
            enemy.GetComponentInChildren<HitEffect>().SpawnEffect();
            enemy.GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
        }
    }

    protected void RoundAttack(Transform AttackPos, float AttackRange, float AttackDamage, Vector2 KnockBackDirection, KnockBack.KnockbackType Type)
    {
        hit = false;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy == coll)
            {
                continue;
            }
            hit = true;
            this.spriteRenderer.sortingOrder = 1;
            this.playerRage.GetRage(5f);
            enemy.GetComponentInParent<PlayerHealth>().TakeDamage(AttackDamage, KnockBackDirection, Type);
            enemy.GetComponentInChildren<HitEffect>().SpawnEffect();
            enemy.GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
        }
    }
}
