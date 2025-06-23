using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckHit : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemies;
    protected SpriteRenderer spriteRenderer;
    protected PlayerRage playerRage;
    protected PlayerState playerState;
    protected Collider2D coll;
    protected List<Collider2D> hitEnemiesThisFrame = new List<Collider2D>();
    public bool hit = false;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
        playerRage = GetComponentInParent<PlayerRage>();
        playerState = GetComponentInParent<PlayerState>();
        coll = GetComponent<Collider2D>();
    }

    protected void StraightAttack(Transform AttackPos, Vector2 AttackSize, float Angle, float AttackDamage, Vector2 KnockBackDirection, KnockBack.KnockbackType Type)
    {
        hit = false;
        hitEnemiesThisFrame.Clear();
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPos.position, AttackSize, Angle, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy == coll)
            {
                continue; 
            }
            hit = true;
            hitEnemiesThisFrame.Add(enemy);
            this.spriteRenderer.sortingOrder = 1;
            this.playerRage.GetRage(5f);
            enemy.GetComponentInParent<PlayerHealth>().TakeDamage(AttackDamage, KnockBackDirection, Type);
            enemy.GetComponent<HitEffect>().SpawnEffect();
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    protected void RoundAttack(Transform AttackPos, float AttackRange, float AttackDamage, Vector2 KnockBackDirection, KnockBack.KnockbackType Type)
    {
        hit = false;
        hitEnemiesThisFrame.Clear();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy == coll)
            {
                continue;
            }
            hit = true;
            hitEnemiesThisFrame.Add(enemy);
            this.spriteRenderer.sortingOrder = 1;
            this.playerRage.GetRage(5f);
            enemy.GetComponentInParent<PlayerHealth>().TakeDamage(AttackDamage, KnockBackDirection, Type);
            enemy.GetComponent<HitEffect>().SpawnEffect();
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}
