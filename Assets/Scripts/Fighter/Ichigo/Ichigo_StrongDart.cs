using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_StrongDart : Projectile
{
    private Vector2 hitPos;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(skill());
    }

    private IEnumerator skill()
    {
        this.transform.localScale += new Vector3(1.5f, 0, 0);
        for (int i = 0; i < 18; i++)
        {
            yield return new WaitForSeconds(0.075f);
            this.transform.rotation *= Quaternion.Euler(5f, 0, 0);
            speed -= 1f;
        }
    }
    protected override void ProjectileMove()
    {
        rb.velocity = transform.right * speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerState enemyState = collision.GetComponentInParent<PlayerState>();
        if (collision.gameObject == owner || enemyState.immuneToDamage) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            hitPos = collision.ClosestPoint(transform.position);
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            Vector2 knockDir = new Vector2(transform.right.x, transform.up.y).normalized;
            playerHealth.TakeDamage(attackDamage, knockDir, KnockBack.KnockbackType.BlownUp); 
            HitEffect hitEffect = collision.GetComponent<HitEffect>();
            hitEffect.HitEffectSpawn(HitEffect.HitEffectType.NormalHit, hitPos);
            HitStopController.Instance.HitStop();
            WhenHit();
        }

        Debug.Log(collision.name);
    }

    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation);
    }

    protected override void WayToDestroy()
    {
        if(transform.rotation.eulerAngles.x >= 90f)
        {
            Destroy(this.gameObject);
        }
    }

}
