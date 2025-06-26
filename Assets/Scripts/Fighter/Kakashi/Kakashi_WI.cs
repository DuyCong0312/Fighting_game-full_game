using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_WI : Projectile
{
    private int hitCount = 0;

    protected override void Start()
    {
        base.Start();
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
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            hitCount++;
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            if (hitCount >= 4)
            {
                playerHealth.TakeDamage(10f, new Vector2(this.transform.right.x, this.transform.up.y), KnockBack.KnockbackType.BlownUp);
            }
            else
            {
                playerHealth.TakeDamage(attackDamage, this.transform.right, KnockBack.KnockbackType.Linear);
            }
            WhenHit();
        }

        Debug.Log(collision.name);
    }

    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation);
    }
}

