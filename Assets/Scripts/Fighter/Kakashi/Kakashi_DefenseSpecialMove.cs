using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_DefenseSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private KnockBack knockBack;
    private PlayerStateMachine player;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("S+J Skill")]
    [SerializeField] private float wjForceP1;
    [SerializeField] private float wjForceP2;

    [Header("S+U Skill")]
    [SerializeField] private GameObject suSkillPrefab;
    [SerializeField] private Transform suSkillPos;

    [Header("S+I Skill")]
    [SerializeField] private GameObject siSkillPrefab;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        knockBack = GetComponentInParent<KnockBack>();
        playerState = GetComponentInParent<PlayerState>();
        player = GetComponentInParent<PlayerStateMachine>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
    }
    private void ActiveKakashiSJskillP1()
    {
        rb.gravityScale = 0f;
        effectAfterImage.StartAfterImageEffect();
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.groundDash, player.dashPos, Quaternion.Euler(0, 180, 0) * player.transform.rotation);
        float direction = playerState.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * wjForceP1, 0f);
    }

    private void ActiveKakashiSJskillP2()
    {
        StartCoroutine(SjskillEnuP2());
    }

    private IEnumerator SjskillEnuP2()
    {
        this.gameObject.layer = player.dashLayer;
        float direction = playerState.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * wjForceP2, 0f);
        while (playerState.isUsingSkill)
        {
            yield return null;
        }
        effectAfterImage.StopAfterImageEffect();
        this.gameObject.layer = player.defaultLayer;
        rb.gravityScale = player.originalGravity;
        rb.velocity = Vector2.zero;
        if (direction == -1f)
        {
            playerState.isFacingRight = true;
            transform.parent.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            playerState.isFacingRight = false;
            transform.parent.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

    }


    private void ActiveKakashiSUskill()
    {
        GameObject suSkill = Instantiate(suSkillPrefab, suSkillPos.position, suSkillPos.transform.rotation);
        SkillCheckHitUseOverLap skillCheck = suSkill.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }

    private void ActiveKakashiSIskill()
    {
        GameObject siSkill = Instantiate(siSkillPrefab, knockBack.opponentDirection.position, Quaternion.identity);
        SkillCheckHitUseOverLap skillCheck = siSkill.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }
}
