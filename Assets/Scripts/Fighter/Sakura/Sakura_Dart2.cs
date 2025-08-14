using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Dart2 : Projectile
{
    private Vector2 hitPos;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            PlayerState enemyState = collision.GetComponentInParent<PlayerState>();
            if (enemyState.immuneToDamage) return;
            hitPos = collision.ClosestPoint(transform.position);
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, this.transform.right, KnockBack.KnockbackType.Linear);
            HitEffect hitEffect = collision.GetComponent<HitEffect>();
            hitEffect.HitEffectSpawn(HitEffect.HitEffectType.SlashHit, hitPos);
            Destroy(this.gameObject);
            WhenHit();
        }
    }

    protected override void ProjectileMove()
    {
        Vector2 movement;
        float yRotation = transform.rotation.eulerAngles.y;
        int directionAngle = yRotation == 0f ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (45f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }
}
