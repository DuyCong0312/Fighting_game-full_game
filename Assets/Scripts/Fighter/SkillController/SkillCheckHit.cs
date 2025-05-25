using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillCheckHit : MonoBehaviour
{
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private Vector2 force;
    [SerializeField] private GameObject effect;
    private GameObject owner;

    private void DisableTrigger()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }
    }

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
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, new Vector2 (this.transform.right.x * force.x, force.y),KnockBack.KnockbackType.Arc);
            Vector2 hitPoint = collision.ClosestPoint(this.transform.position);
            WhenHit(new Vector2 (hitPoint.x, hitPoint.y + 1f));
        }
    }
    private void WhenHit(Vector2 hitPosition)
    {
        Instantiate(effect, hitPosition, transform.rotation);
    }
}
