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
    [SerializeField] private GameObject siSkillPrefab01;
    [SerializeField] private GameObject siSkillPrefab02;
    [SerializeField] private GameObject siSkillPrefab03;
    [SerializeField] private GameObject siLastSkillPrefab;
    [SerializeField] private Transform siSkillPos;
    [SerializeField] private float siSpeed;
    [SerializeField] private float siForce;
    private float originalGravity;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        player = GetComponentInParent<PlayerStateMachine>();
        knockBack = GetComponentInParent<KnockBack>();
        groundCheck = GetComponentInParent<CheckGround>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
        originalGravity = rb.gravityScale;
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
        Instantiate(siSkillPrefab01, siSkillPos.position, siSkillPos.rotation);
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
        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
    }

    private void ActiveNarutoSIskill03()
    {
        StartCoroutine(NarutoSIskill03());
    }

    private IEnumerator NarutoSIskill03()
    {
        Transform opponent = knockBack.opponentDirection;
        float direction = opponent.eulerAngles.y == 0f ? 1f : -1f;

        Vector2 pos1 = new Vector2(opponent.position.x - 3f * direction, opponent.position.y);
        Vector2 pos1P = new Vector2(opponent.position.x + 3f * direction, opponent.position.y);
        Vector2 pos2 = new Vector2(opponent.position.x + 1.5f * direction, opponent.position.y);
        Vector2 pos2P = new Vector2(opponent.position.x - 1.5f * direction, opponent.position.y);

        Quaternion rot1 = opponent.rotation * Quaternion.Euler(0, 0, 0);
        Quaternion rot2 = opponent.rotation * Quaternion.Euler(0, 180, 0);
        yield return null;

        Instantiate(siSkillPrefab02, pos1, rot1);
        Instantiate(siSkillPrefab02, pos1P, rot2);
        yield return new WaitForSeconds(0.25f);

        Instantiate(siSkillPrefab03, pos2, rot1);
        Instantiate(siSkillPrefab03, pos2P, rot2);

        yield return new WaitForSeconds(0.25f);
        ActiveNarutoSIskill03P2();
    }

    private void ActiveNarutoSIskill03P2()
    {
        Transform opponent = knockBack.opponentDirection;
        Vector2 newPosition = new Vector2(opponent.position.x , opponent.position.y + 3f);
        transform.parent.position = newPosition;
    }

    private void ActiveNarutoSIskill04()
    {
    }

    private IEnumerator SIskill04()
    {
        rb.velocity = new Vector2(0f, -siForce);
        while (true)
        {
            yield return null;
            if (groundCheck.isGround)
            {
                break;
            }
        }
        GameObject skill = Instantiate(siLastSkillPrefab, new Vector2(this.transform.position.x, this.transform.position.y - 0.25f), Quaternion.identity);
        SkillCheckHitUseOverLap skillCheck = skill.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck != null) 
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }
}
