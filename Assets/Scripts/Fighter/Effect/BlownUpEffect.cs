using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlownUpEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(3f, new Vector2(this.transform.right.x, this.transform.up.y), KnockBack.KnockbackType.BlownUp);
        }
    }
}
