using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Naruto_DefenseSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private PlayerStateMachine player;
    private KnockBack knockBack;
    private CheckGround groundCheck;
    private SpawnEffectAfterImage effectAfterImage;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    [Header("S+J Skill")]
    [SerializeField] private float speed;

    [Header("S+U Skill")]
    [SerializeField] private GameObject suSkillPrefab;
    [SerializeField] private Transform suSkillPos;

    [Header("S+I Skill")]
    [SerializeField] private GameObject siSkillEffectPrefab01;
    [SerializeField] private GameObject siSkillEffectPrefab02;
    [SerializeField] private Transform siSkillEffectPos01;
    [SerializeField] private Transform siSkillEffectPos02;
    [SerializeField] private GameObject siSkillEffectPrefab;
    [SerializeField] private GameObject siSkillPrefab01;
    [SerializeField] private GameObject siSkillPrefab02;
    [SerializeField] private GameObject siSkillPrefab03;
    [SerializeField] private GameObject siLastSkillPrefab;
    [SerializeField] private Transform siSkillPos;
    [SerializeField] private float siSpeed;
    [SerializeField] private float siForce;
    private GameObject skillObject;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        player = GetComponentInParent<PlayerStateMachine>();
        knockBack = GetComponentInParent<KnockBack>();
        groundCheck = GetComponentInParent<CheckGround>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void ActiveNarutoSJskill()
    {
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        effectAfterImage.StartAfterImageEffect();
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.groundDash, player.dashPos, Quaternion.Euler(0, 180, 0) * playerState.transform.rotation);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.dash);
    }

    private void ActiveNarutoSUskill()
    {
        StartCoroutine(NarutoSUskillEnum());
    }

    private IEnumerator NarutoSUskillEnum()
    {
        GameObject skill = Instantiate(suSkillPrefab, suSkillPos.position, suSkillPos.rotation);
        Projectile skillCheck = skill.GetComponent<Projectile>();
        if (skillCheck != null) 
        {
            skillCheck.SetOwner(this.gameObject);
        }
        yield return new WaitForSeconds(0.75f);
        playerState.isUsingSkill = false;

    }

    private void ActiveNarutoSIskill01P1()
    {
        Instantiate(siSkillEffectPrefab01, siSkillEffectPos01.position, siSkillEffectPos01.rotation);
        Instantiate(siSkillEffectPrefab02, siSkillEffectPos02.position, siSkillEffectPos02.rotation);
    }

    private void ActiveNarutoSIskill01P2()
    {
        skillObject = Instantiate(siSkillEffectPrefab, siSkillPos.position, siSkillPos.rotation, siSkillPos);
    }

    private void ActiveNarutoSIskill02()
    {
        StartCoroutine(SIskillMoveEnum(siSpeed, 0.1f));
    }

    private IEnumerator SIskillMoveEnum(float force, float delay)
    {
        rb.gravityScale = 0f;
        yield return null;
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * force, rb.velocity.y);
        yield return new WaitForSeconds(delay);
        rb.gravityScale = player.originalGravity;
        rb.velocity = Vector2.zero;
        spriteRenderer.enabled = false;
        Destroy(skillObject);
    }

    private void ActiveNarutoSIskill03()
    {
        StartCoroutine(NarutoSIskill03());
    }

    private IEnumerator NarutoSIskill03()
    {
        anim.speed = 0f;
        yield return null;
        Transform opponent = knockBack.opponentDirection;
        float direction = opponent.eulerAngles.y == 0f ? 1f : -1f; 

        Vector2 pos1 = new Vector2(opponent.position.x - 2f * direction, opponent.position.y);
        Vector2 pos1P = new Vector2(opponent.position.x + 2f * direction, opponent.position.y);
        Vector2 pos2 = new Vector2(opponent.position.x - 1f * direction, opponent.position.y);
        Vector2 pos2P = new Vector2(opponent.position.x + 1f * direction, opponent.position.y);

        Quaternion rot1 = opponent.rotation * Quaternion.Euler(0, 0, 0);
        Quaternion rot2 = Quaternion.Euler(0, opponent.rotation.eulerAngles.y == 0f ? 180 : 0, 0);

        GameObject obj1 = Instantiate(siSkillPrefab02, pos1, rot1); 
        SkillCheckHitUseOverLap skillCheckObj01 = obj1.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheckObj01 != null)
        {
            skillCheckObj01.SetOwner(this.gameObject);
        }
        GameObject obj2 = Instantiate(siSkillPrefab02, pos1P, rot2);
        SkillCheckHitUseOverLap skillCheckObj02 = obj2.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheckObj02 != null)
        {
            skillCheckObj02.SetOwner(this.gameObject);
        }
        yield return new WaitForSeconds(0.5f);

        GameObject obj3 = Instantiate(siSkillPrefab03, pos2, rot1);
        SkillCheckHitUseOverLap skillCheckObj03 = obj3.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheckObj03 != null)
        {
            skillCheckObj03.SetOwner(this.gameObject);
        }
        GameObject obj4 = Instantiate(siSkillPrefab03, pos2P, rot2);
        SkillCheckHitUseOverLap skillCheckObj04 = obj4.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheckObj04 != null)
        {
            skillCheckObj04.SetOwner(this.gameObject);
        }

        yield return new WaitForSeconds(0.5f);
        anim.speed = 1f;
        ActiveNarutoSIskill03P2();
        yield return null;
        spriteRenderer.enabled = true;
    }

    private void ActiveNarutoSIskill03P2()
    {
        Transform opponent = knockBack.opponentDirection;
        Vector2 newPosition = new Vector2(opponent.position.x , opponent.position.y + 3f);
        transform.parent.position = newPosition;
    }

    private void ActiveNarutoSIskill04()
    {
        StartCoroutine(SIskill04());
    }

    private IEnumerator SIskill04()
    {
        anim.speed = 0f;
        yield return null;
        GameObject skill01 = Instantiate(siSkillPrefab01, siSkillPos.position, Quaternion.identity, this.transform);
        SkillCheckHitUseOverLap skillCheck01 = skill01.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck01 != null)
        {
            skillCheck01.SetOwner(this.gameObject);
        }
        yield return null;
        rb.velocity = new Vector2(0f, -siForce);
        while (true)
        {
            yield return null;
            if (groundCheck.isGround)
            {
                playerState.isUsingSkill = false;
                anim.speed = 1f;
                break;
            }
        }
        Destroy(skill01);
        GameObject skill02 = Instantiate(siLastSkillPrefab, new Vector2(this.transform.position.x, this.transform.position.y - 0.25f), Quaternion.identity);
        SkillCheckHitUseOverLap skillCheck02 = skill02.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck02 != null) 
        {
            skillCheck02.SetOwner(this.gameObject);
        }
    }
}
