using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_SI : SkillCheckHitUseOverLap
{
    [SerializeField] private float skillRange;
    [SerializeField] private GameObject skillEffect;
    private List<Collider2D> totalEnemiesHit = new List<Collider2D>();

    protected override void Start()
    {
        base.Start();
        StartCoroutine(SkillCheckEnum(5, 0.2f));
    }

    private IEnumerator SkillCheckEnum(int count, float delay)
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < count; i++)
        {
            RoundAttack(this.transform, skillRange, 4f, Vector2.zero, KnockBack.KnockbackType.Linear); 
            foreach (var enemy in hitEnemiesThisFrame)
            {
                if (!totalEnemiesHit.Contains(enemy))
                    totalEnemiesHit.Add(enemy);
            }
            yield return new WaitForSeconds(delay);
        } 
        
        while (true)
        {
            this.transform.localScale -= new Vector3(0.05f, 0.05f, 0f);
            if (transform.localScale.x <= 0.45f)
            {
                foreach (Collider2D enemy in totalEnemiesHit)
                {
                    enemy.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            if (this.transform.localScale.x <= 0f)
            {
                SpawnEffect();
                foreach (Collider2D enemy in totalEnemiesHit)
                {
                    enemy.GetComponent<SpriteRenderer>().enabled = true;
                }
                yield return null;
                Destroy(this.gameObject);
                yield break;
            }
            yield return null;
        }
    }

    private void SpawnEffect()
    {
        Instantiate(skillEffect, this.transform.position, Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(this.transform.position, skillRange);
    }

}
