using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakuraCom_Uskill : MonoBehaviour
{
    [Header("U Skill")]
    [SerializeField] private GameObject SkillPrefab;
    [SerializeField] private Transform skillPos;
    [SerializeField] private Transform newPos;
    [SerializeField] private float backSpeed;

    private Rigidbody2D rb;
    private SpawnEffectAfterImage effectAfterImage;
    private Vector3 newPosition;
    private bool canMove = true;
    private float originalGravity;
    private Vector2 movement;
    private Coroutine stepBackCoroutine;

    [Header("U+K Skill")]
    [SerializeField] private float force;
    [SerializeField] private GameObject effectUK1;
    [SerializeField] private GameObject effectUK2;
    [SerializeField] private Transform spwanEffectPos;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
    }

    private void ActiveStepBack()
    {
        if (stepBackCoroutine == null)
        {
            stepBackCoroutine = StartCoroutine(StepBack());
            effectAfterImage.StartAfterImageEffect();
        }
    }
    private IEnumerator StepBack()
    {
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        newPosition = newPos.position;
        while (Vector2.Distance(transform.parent.position, newPosition) > 0.01f && canMove)
        {
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, newPosition, backSpeed * Time.deltaTime);
            yield return null;
        }
        StopStepBack();
    }

    private void StopStepBack()
    {
        rb.gravityScale = originalGravity;
        effectAfterImage.StopAfterImageEffect();
        canMove = true;
        stepBackCoroutine = null;
    }

    private void ActiveSakuraUSkill(int extraInstances, float offset)
    {
        Instantiate(SkillPrefab, skillPos.position, transform.rotation);

        for (int i = 1; i <= extraInstances; i++)
        {
            Instantiate(SkillPrefab, new Vector2(skillPos.position.x, skillPos.position.y + offset), transform.rotation);
            Instantiate(SkillPrefab, new Vector2(skillPos.position.x, skillPos.position.y - offset), transform.rotation);
        }
    }

    private void ActiveSakuraUSkillP1()
    {
        ActiveSakuraUSkill(0, 0f);
    }

    private void ActiveSakuraUSkillP2()
    {
        ActiveSakuraUSkill(1, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(CONSTANT.MainCamera) || collision.collider.CompareTag(CONSTANT.MapBorder))
        {
            if (stepBackCoroutine != null)
            {
                StopCoroutine(stepBackCoroutine);
                StopStepBack();
            }
        }
    }

    private void ActiveSakuraUKSkill()
    {
        this.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 0, 45);
        Vector2 direction = -this.transform.up;
        float angle = Mathf.Atan2(direction.y, direction.x);
        movement.x = force * Mathf.Cos(angle);
        movement.y = force * Mathf.Sin(angle);

        rb.velocity = movement.normalized * force;
        effectAfterImage.StartAfterImageEffect();
    }

    private void ActiveEffectUK()
    {
        SpawnSkillEffect(effectUK1);
        SpawnSkillEffect(effectUK2);
    }

    private void SpawnSkillEffect(GameObject name)
    {
        Instantiate(name, spwanEffectPos.position, spwanEffectPos.rotation);
    }

}
