using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_SU : Projectile
{
    private int hitCount = 0;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float damageInterval = 0.5f;
    private float damageTimer = 0f;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageTimer = 0f;
    }

    protected override void Update()
    {
        timeExist -= Time.deltaTime;
        WayToDestroy();
        if (hitCount >= 4)
        {
            Color color = spriteRenderer.color;
            color.a -= Time.deltaTime * 0.75f;
            spriteRenderer.color = color;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        return;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            damageTimer -= Time.deltaTime;
            StartCoroutine(SpeedWhenHit(2.5f));
            if (damageTimer <= 0f && hitCount < 4)
            {
                hitCount++;
                PlayerHealth playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();
                if (playerHealth != null)
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
        while (hitCount < 4)
        {
            rb.velocity = this.transform.right * speed2;
            yield return null;
        }
        rb.gravityScale = 0.5f;
        rb.velocity = Vector2.zero;
    }

    protected override void WayToDestroy()
    {
        if (timeExist <= 0 || spriteRenderer.color.a <= 0.01f)
        {
            Destroy(this.gameObject);
        }
    }
}
