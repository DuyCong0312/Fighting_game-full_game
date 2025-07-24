using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_WI : Projectile
{
    private int hitCount = 0;
    [SerializeField] private float damageInterval = 0.5f;
    private float damageTimer = 0f;

    protected override void Start()
    {
        base.Start();
        damageTimer = 0f;
    }

    protected override void Update()
    {
        return;
    }

    protected override void ProjectileMove()
    {
        rb.velocity = transform.right * speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        return;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            damageTimer -= Time.deltaTime;
            StartCoroutine(SpeedWhenHit(7.5f));
            if (damageTimer <= 0f && hitCount < 6)
            {
                hitCount++;
                PlayerHealth playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();
                if (hitCount == 6)
                {
                    playerHealth.TakeDamage(5f, new Vector2(this.transform.right.x, this.transform.up.y), KnockBack.KnockbackType.BlownUp);
                }
                else
                {
                    playerHealth.TakeDamage(attackDamage, this.transform.right, KnockBack.KnockbackType.Linear);
                }
                WhenHit();
                damageTimer = damageInterval;
            }
        }
    }

    private IEnumerator SpeedWhenHit(float speed2)
    {
        yield return null;
        rb.velocity = this.transform.right * speed2;
    }

    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation);
    }
}

