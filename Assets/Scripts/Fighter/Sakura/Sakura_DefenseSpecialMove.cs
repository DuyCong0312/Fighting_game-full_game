using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_DefenseSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("S+J Skill")]
    [SerializeField] private GameObject sjSkillPrefab;
    [SerializeField] private Transform sjSkillPos;

    [Header("S+U Skill")]
    [SerializeField] private GameObject suSkillEffect;
    [SerializeField] private Transform suSkillPos;

    [Header("S+I Skill")]
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
    }

    private void ActiveSakuraSJSkill()
    {
        Instantiate(sjSkillPrefab, sjSkillPos.position, transform.rotation);
    }
    
    private void ActiveSakuraSUSkill()
    {
        Instantiate(suSkillEffect, suSkillPos.position, suSkillEffect.transform.rotation);
    }
    private void ActiveSakuraSISkill()
    {
        effectAfterImage.StartAfterImageEffect();
        rb.velocity = new Vector2(this.transform.right.x * speed, rb.velocity.y);
    }
}
