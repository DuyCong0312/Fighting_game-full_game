using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renji_dart : Projectile
{
    public Transform target;

    protected override void Start()
    {
        ProjectileMove();
    }

    protected override void Update()
    {
        StartCoroutine(Move()); 
        transform.Rotate(0, 0, 720f * Time.deltaTime);
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(0.5f);
        if (target != null)
        {
            Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    protected override void ProjectileMove()
    {
        return;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, new Vector2 (collision.transform.right.x, collision.transform.up.y), KnockBack.KnockbackType.Linear);
            WayToDestroy();
            WhenHit();
        }

        Debug.Log(collision.name);
    }

    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
    }

    protected override void WayToDestroy()
    {
        Destroy(this.gameObject);
    }
}
