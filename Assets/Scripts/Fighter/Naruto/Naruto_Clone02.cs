using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_Clone02 : SkillCheckHitUseOverLap
{
    private Rigidbody2D rb;

    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float speed;
    [SerializeField] private float angleDir;
    [SerializeField] private float time;
    [SerializeField] private float timeExist = 2f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private Vector2 knockBackDir;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CLoneEnum()); 
        Spawn();
    }

    private void Update()
    {
        timeExist -= Time.deltaTime;
        if (timeExist <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator CLoneEnum()
    {
        yield return new WaitForSeconds(0.2f);
        CalculateVelocity();
        yield return null;
        while (true)
        {
            StraightAttack(attackPos, attackSize, 0f, 5f, knockBackDir, KnockBack.KnockbackType.Linear);
            yield return null;
            if (hit)
            {
                break;
            }
        }
        Spawn();
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void CalculateVelocity()
    {
        Vector2 movement;
        float yRotation = transform.rotation.eulerAngles.y;
        int directionAngle = yRotation == 0f ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (angleDir * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }

    private void Spawn()
    {
        Instantiate(spawnPrefab, spawnPos.position, spawnPos.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }

}
