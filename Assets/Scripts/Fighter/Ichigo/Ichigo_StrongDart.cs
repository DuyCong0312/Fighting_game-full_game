using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_StrongDart : Projectile
{
    protected override void ProjectileMove()
    {
        rb.velocity = transform.right * speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger by: {gameObject.name}, hit: {collision.name} at {Time.time}");
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            Vector2 knockDir = new Vector2(transform.right.x, transform.up.y).normalized;
            playerHealth.TakeDamage(attackDamage, knockDir, KnockBack.KnockbackType.BlownUp);
            Debug.Log($"Apply Knockback: {knockDir}");
            WhenHit();
        }

        Debug.Log(collision.name);
    }

    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation);
    }

}
