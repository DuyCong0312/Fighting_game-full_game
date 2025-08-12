using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Rock : Projectile
{
    [SerializeField] private GameObject hitEffect;

    protected override void ProjectileMove()
    {
        Vector2 movement;
        float yRotation = transform.rotation.eulerAngles.y;
        int directionAngle = yRotation == 0f ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (25f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerState enemyState = collision.GetComponentInParent<PlayerState>();
        if (collision.gameObject == owner || enemyState.immuneToDamage) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, this.transform.right * 3f,KnockBack.KnockbackType.Linear);
            Vector2 hitPoint = collision.ClosestPoint(this.transform.position);
            WhenHitPlayer(hitPoint);
        }
        else if (collision.gameObject.CompareTag(CONSTANT.Ground))
        {
            Destroy(this.gameObject);
            WhenHit();
        }
    }

    private void WhenHitPlayer(Vector2 hitPosition)
    {
        Instantiate(hitEffect, hitPosition, transform.rotation);
    }
    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation);
    }

}
