using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillCheckHit : MonoBehaviour
{
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private Vector2 force;
    [SerializeField] private bool useHitStop;
    [SerializeField] private bool useSlashHitEffect;
    private GameObject owner; 
    private bool hasHit = false;

    private void IgnoreCollision()
    {
        Collider2D myCollider = GetComponent<Collider2D>();
        Collider2D ownerCollider = owner.GetComponent<Collider2D>();

        if (myCollider != null && ownerCollider != null)
        {
            Physics2D.IgnoreCollision(myCollider, ownerCollider, true);
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
        IgnoreCollision();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            hasHit = true;
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, new Vector2 (this.transform.right.x * force.x, force.y),KnockBack.KnockbackType.Arc);
            Vector2 hitPoint = collision.ClosestPoint(this.transform.position);
            WhenHit(collision, new Vector2(hitPoint.x, hitPoint.y + 1f));
            CallHitStop();
        }
    }

    private void WhenHit(Collider2D collision, Vector2 hitPosition)
    {
        HitEffect hitEffect = collision.GetComponent<HitEffect>();
        Debug.Log(collision.name);
        if (hitEffect == null) 
        {
            return;
        }
        if (useSlashHitEffect)
        {
            hitEffect.HitEffectSpawn(HitEffect.HitEffectType.SlashHit, hitPosition);
        }
        else
        {
            hitEffect.HitEffectSpawn(HitEffect.HitEffectType.NormalHit, hitPosition);
        }
    }

    private void CallHitStop()
    {
        if (useHitStop)
        {
            HitStopController.Instance.HitStop();
        }
    }
}
