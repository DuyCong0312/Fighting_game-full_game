using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Renji_DefenseSpecialMove : MonoBehaviour
{
    private KnockBack knockBack;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerState playerState;
    private CheckGround groudCheck;
    private Animator anim;

    [Header("S+J Skill")]
    [SerializeField] private GameObject SjEffectPrefab;
    [SerializeField] private Transform SjSkillPos;
    [SerializeField] private float force;
    private bool isActiveSjJumpForce = false;

    [Header("S+U Skill")]
    [SerializeField] private GameObject SuSkillPrefab;

    [Header("S+I Skill")]
    [SerializeField] private GameObject SiEffect1Prefab;
    [SerializeField] private GameObject SiEffect2Prefab;
    [SerializeField] private GameObject SiEffect3Prefab;
    [SerializeField] private Transform Si1SkillPos;
    [SerializeField] private Transform Si2SkillPos;
    [SerializeField] private Transform Si3SkillPos;

    private void Start()
    {
        knockBack = GetComponentInParent<KnockBack>();
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        groudCheck = GetComponentInParent<CheckGround>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(!groudCheck.isGround && !isActiveSjJumpForce)
        {
            isActiveSjJumpForce = true;
        }
        if (groudCheck.isGround && isActiveSjJumpForce)
        {
            anim.speed = 1f;
            isActiveSjJumpForce = false;
        }
    }

    private void ActiveRenjiSJSkill()
    {
        CalForce();
        anim.speed = 0f;
    }

    private void SpawnRenjiSJEffectSkill()
    {
        Instantiate(SjEffectPrefab, SjSkillPos.position, this.transform.rotation);
    }

    private void CalForce()
    {

        Vector2 movement;
        int directionAngle = playerState.isFacingRight ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (45f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * force;
    }

    private void ActiveRenjiSUSkill()
    {
        Instantiate(SuSkillPrefab, new Vector2(knockBack.opponentDirection.position.x, spriteRenderer.bounds.min.y), this.transform.rotation);
    }

    private void ActiveRenjiSI1skill()
    {
        Instantiate(SiEffect1Prefab, Si1SkillPos.position, Quaternion.identity);
    }

    private void ActiveRenjiSI2skill()
    {
        Instantiate(SiEffect2Prefab, Si2SkillPos.position, this.transform.rotation);
    }

    private void ActiveRenjiSI3skill()
    {
        Instantiate(SiEffect3Prefab, Si3SkillPos.position, this.transform.rotation);
    }
}
