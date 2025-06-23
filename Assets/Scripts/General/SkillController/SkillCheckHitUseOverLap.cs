using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheckHitUseOverLap : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemies;
    protected SpriteRenderer ownerSpriteRenderer;
    protected PlayerRage ownerRage;
    protected PlayerState ownerState;
    protected Collider2D ownerColl;
    protected Animator ownerAnim;
    protected List<Collider2D> hitEnemiesThisFrame = new List<Collider2D>();
    public bool hit = false;
    public GameObject owner;

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    protected virtual void Start()
    {
        ownerSpriteRenderer = owner.GetComponent<SpriteRenderer>();
        ownerRage = owner.GetComponentInParent<PlayerRage>();
        ownerState = owner.GetComponentInParent<PlayerState>();
        ownerColl = owner.GetComponent<Collider2D>();
        ownerAnim = owner.GetComponent<Animator>();
    }


    protected void StraightAttack(Transform AttackPos, Vector2 AttackSize, float Angle, float AttackDamage, Vector2 KnockBackDirection, KnockBack.KnockbackType Type)
    {
        hit = false;
        hitEnemiesThisFrame.Clear();
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPos.position, AttackSize, Angle, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy == ownerColl)
            {
                continue;
            }
            hit = true;
            hitEnemiesThisFrame.Add(enemy);
            ownerSpriteRenderer.sortingOrder = 1;
            ownerRage.GetRage(5f);
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
            if (enemy == ownerColl)
            {
                continue;
            }
            hit = true;
            hitEnemiesThisFrame.Add(enemy);
            ownerSpriteRenderer.sortingOrder = 1;
            ownerRage.GetRage(5f);
            enemy.GetComponentInParent<PlayerHealth>().TakeDamage(AttackDamage, KnockBackDirection, Type);
            enemy.GetComponent<HitEffect>().SpawnEffect();
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}
