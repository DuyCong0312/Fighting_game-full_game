using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart: MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    [SerializeField] protected float attackDamage = 5f;
    [SerializeField] protected float timeExist = 2f;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected GameObject effect;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    protected virtual void Update()
    {
        timeExist -= Time.deltaTime;
        DestroyByTime();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.Player) || collision.gameObject.CompareTag(CONSTANT.Com))
        {
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage, this.transform.right);
            Destroy(this.gameObject);
            WhenHit();
        }
        
        Debug.Log(collision.name);
    }

    protected virtual void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
    }

    protected virtual void DestroyByTime()
    {
        if(timeExist <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
