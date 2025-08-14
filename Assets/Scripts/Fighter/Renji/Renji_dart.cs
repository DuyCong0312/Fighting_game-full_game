using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Renji_dart : Projectile
{
    public float moveSpeed = 0f;
    [SerializeField] private float forwardTime = 0.5f;

    private Transform returnPos;
    private int hitCount = 0;

    public void SetReturnPos(Transform returnPos)
    {
        this.returnPos = returnPos;
    }

    protected override void Update()
    {
        transform.Rotate(0, 0, 720f * Time.deltaTime);
    }

    protected override void ProjectileMove()
    {
        StartCoroutine(MoveAndReturn());
    }

    private IEnumerator MoveAndReturn()
    {
        while (moveSpeed == 0f)
        {
            yield return null;
        }
        CalVeclocity();
        yield return new WaitForSeconds(forwardTime); 
        rb.velocity = Vector2.zero;
        while (Vector2.Distance(transform.position, returnPos.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, returnPos.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        WayToDestroy();
    }

    private void CalVeclocity()
    {
        Vector2 movement;
        float yRotation = transform.rotation.eulerAngles.y;
        int directionAngle = yRotation == 0f ? 1 : -1;
        Vector2 direction = owner.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (returnPos.eulerAngles.z * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * moveSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            PlayerState enemyState = collision.GetComponentInParent<PlayerState>();
            if (enemyState.immuneToDamage) return;
            Vector2 hitPos = collision.ClosestPoint(transform.position);
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            HitEffect hitEffect = collision.GetComponent<HitEffect>();
            if (hitCount < 2)
            {
                hitCount++;
                if (hitCount == 2)
                {
                    playerHealth.TakeDamage(attackDamage, new Vector2(this.transform.right.x, this.transform.up.y), KnockBack.KnockbackType.BlownUp);
                }
                else
                {
                    playerHealth.TakeDamage(attackDamage, this.transform.right, KnockBack.KnockbackType.Linear);
                   
                } 
                hitEffect.HitEffectSpawn(HitEffect.HitEffectType.SlashHit, hitPos);
            }
        }

        Debug.Log(collision.name);
    }

    protected override void WayToDestroy()
    {
        Destroy(this.gameObject);
    }
}
