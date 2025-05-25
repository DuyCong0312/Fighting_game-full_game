using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_DefenseSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("S+J Skill")]
    [SerializeField] private GameObject sjSkillPrefab;
    [SerializeField] private Transform sjSkillPos;

    [Header("S+U Skill")]
    [SerializeField] private float force;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("S+I Skill")]
    [SerializeField] private GameObject siSkillPrefab;
    [SerializeField] private Transform siSkillPos;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
    }

    private void ActiveIchigoSJSkill()
    {
        float offset = 0.55f;
        GameObject skillRight = Instantiate(sjSkillPrefab, new Vector2(sjSkillPos.position.x + offset, sjSkillPos.position.y), sjSkillPos.localRotation);
        GameObject skillLeft = Instantiate(sjSkillPrefab, new Vector2(sjSkillPos.position.x - offset, sjSkillPos.position.y), sjSkillPos.localRotation * Quaternion.Euler(0, 180, 0));

        SkillCheckHit skillCheckLeft = skillRight.GetComponent<SkillCheckHit>();
        if (skillCheckLeft != null)
            skillCheckLeft.SetOwner(this.gameObject);

        SkillCheckHit skillCheckRight = skillLeft.GetComponent<SkillCheckHit>();
        if (skillCheckRight != null)
            skillCheckRight.SetOwner(this.gameObject);
    }

    private void ActiveIchigoSUSkill()
    {
        effectAfterImage.StartAfterImageEffect();
        rb.velocity = this.transform.right * force;
    }
    private void ActiveIchigoSISkill()
    {
        for (int i = 0; i <= 3; i++)
        {
            Vector3 spawnPos = siSkillPos.position;
            spawnPos.x -= i * 1f;

            GameObject obj = Instantiate(siSkillPrefab, spawnPos, siSkillPos.rotation);

            Vector3 newScale = obj.transform.localScale;
            newScale.y -= i * 0.5f;
            obj.transform.localScale = newScale;

            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color color = sr.color;
                color.a -= i * 0.2f;
                sr.color = color;
            }
            if (i > 0)
            {
                Collider2D col = obj.GetComponent<Collider2D>();
                if (col != null)
                {
                    col.enabled = false;
                }
            }
        }
    }
}
