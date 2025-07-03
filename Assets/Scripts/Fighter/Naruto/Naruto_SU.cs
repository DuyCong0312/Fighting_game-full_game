using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_SU : Projectile
{
    private int hitCount = 0;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        timeExist -= Time.deltaTime;
        WayToDestroy();
        if (hitCount >= 4)
        {
            rb.gravityScale = 0.5f;
            rb.velocity = Vector2.zero;
            Color color = spriteRenderer.color;
            color.a -= Time.deltaTime * 1.5f;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            hitCount++;
            if (hitCount < 4)
            {
                PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
                playerHealth.TakeDamage(attackDamage, this.transform.right, KnockBack.KnockbackType.Linear);
            }
            WhenHit();
        }

        Debug.Log(collision.name);
    }

    protected override void WayToDestroy()
    {
        if (timeExist <= 0 || spriteRenderer.color.a <= 0.01f)
        {
            Destroy(this.gameObject);
        }
    }
}
