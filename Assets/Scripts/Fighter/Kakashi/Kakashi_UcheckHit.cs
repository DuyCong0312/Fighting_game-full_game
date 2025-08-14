using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_UcheckHit : Projectile
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
            WhenHit();
            Destroy(this.gameObject);
        }

        Debug.Log(collision.name);
    }

    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
    }

}
