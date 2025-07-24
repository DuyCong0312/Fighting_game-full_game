using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Dart1 : Projectile
{
    private Vector2 hitPos;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            hitPos = collision.ClosestPoint(transform.position);
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, this.transform.right, KnockBack.KnockbackType.Linear);
            HitEffect hitEffect = collision.GetComponent<HitEffect>();
            hitEffect.HitEffectSpawn(HitEffect.HitEffectType.SlashHit, hitPos);
            Destroy(this.gameObject);
            WhenHit();
        }

        Debug.Log(collision.name);
    }
}
