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
        IchigoSJSkill(0.55f);
        SkillCheckHit skillCheck = GetComponent<SkillCheckHit>();
        if (skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }
    private void IchigoSJSkill(float offset)
    {
        Instantiate(sjSkillPrefab, new Vector2(sjSkillPos.position.x + offset, sjSkillPos.position.y), sjSkillPos.localRotation);
        Instantiate(sjSkillPrefab, new Vector2(sjSkillPos.position.x - offset, sjSkillPos.position.y), sjSkillPos.localRotation * Quaternion.Euler(0, 180, 0));
    }
    private void ActiveIchigoSUSkill()
    {
        effectAfterImage.StartAfterImageEffect();
        rb.velocity = this.transform.right * force;
    }
    private void ActiveIchigoSISkill()
    {
        Instantiate(siSkillPrefab, siSkillPos.position, siSkillPos.rotation);
    }
}
