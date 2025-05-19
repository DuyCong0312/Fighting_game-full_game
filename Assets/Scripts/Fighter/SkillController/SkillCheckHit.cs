using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheckHit : MonoBehaviour
{
    [SerializeField] private float attackDamage = 5f;
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

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;

        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, this.transform.right);
            Destroy(this.gameObject);
            WhenHit();
        }

        Debug.Log(collision.name);
    }
    private void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
    }
}
