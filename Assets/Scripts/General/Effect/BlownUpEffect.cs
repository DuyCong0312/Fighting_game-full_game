using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlownUpEffect : MonoBehaviour
{
    private GameObject owner;
    [SerializeField] private float explosionDamage;

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
            playerHealth.TakeDamage(explosionDamage, new Vector2(this.transform.right.x, this.transform.up.y), KnockBack.KnockbackType.BlownUp);
            HitStopController.Instance.HitStop();
        }
    }
}
