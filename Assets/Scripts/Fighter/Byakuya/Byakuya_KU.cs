using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Byakuya_KU : Projectile
{
    private Vector2 hitPos;

    protected override void Update()
    {
        timeExist -= Time.deltaTime;
        WayToDestroy();
    }

    protected override void ProjectileMove()
    {
        Vector2 movement;
        float yRotation = owner.transform.rotation.eulerAngles.y;
        int directionAngle = yRotation == 0f ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (-60f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            hitPos = collision.ClosestPoint(transform.position);
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, owner.transform.right, KnockBack.KnockbackType.Linear);
            WhenHit(); 
            Destroy(this.gameObject);
        }

        Debug.Log(collision.name);
    }

    protected override void WhenHit()
    {
        Instantiate(effect, hitPos, transform.rotation * Quaternion.Euler(0, 0, 45));
    }

    protected override void WayToDestroy()
    {
        if (timeExist <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
