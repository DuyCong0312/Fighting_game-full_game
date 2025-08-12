using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_darts : Projectile
{
    private Vector2 hitPos;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerState enemyState = collision.GetComponentInParent<PlayerState>();
        if (collision.gameObject == owner || enemyState.immuneToDamage) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            hitPos = collision.ClosestPoint(transform.position);
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, this.transform.right, KnockBack.KnockbackType.Linear); 
            HitEffect hitEffect = collision.GetComponent<HitEffect>();
            hitEffect.HitEffectSpawn(HitEffect.HitEffectType.NormalHit, hitPos);
            HitStopController.Instance.HitStop();
            Destroy(this.gameObject);
            WhenHit();
        }

        Debug.Log(collision.name);
    }

    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation);
    }
}
