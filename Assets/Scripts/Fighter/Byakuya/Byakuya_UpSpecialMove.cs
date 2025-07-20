using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Byakuya_UpSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;
    private Animator anim;

    [Header("W+J Skill")]
    [SerializeField] private float force;
    [SerializeField] private GameObject wjSkillEffect01Prefab;
    [SerializeField] private Transform wj01EffectPos;
    [SerializeField] private GameObject wjSkillEffect02Prefab;
    [SerializeField] private Transform wj02EffectPos;
    [SerializeField] private GameObject wjSkillEffect03Prefab;
    [SerializeField] private Transform wj03EffectPos;

    [Header("W+U Skill")]
    [SerializeField] private float speed;
    private float originalGravity;

    [Header("W+I Skill")]
    [SerializeField] private GameObject wiSkillPrefab;
    [SerializeField] private Transform wiSkillPos;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
        anim = GetComponent<Animator>();
        originalGravity = rb.gravityScale;
    }

    private void ActiveByakuyaWJskill()
    {
        StartCoroutine(WJskillMove());
    }

    private IEnumerator WJskillMove()
    {
        float direction = playerState.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * force, rb.velocity.y);
        rb.gravityScale = 0;
        effectAfterImage.StartAfterImageEffect();
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        rb.gravityScale = originalGravity;
        effectAfterImage.StopAfterImageEffect();
    }

    private void ActiveByakuyaWJEffectskill01()
    {
        GameObject effect01 = Instantiate(wjSkillEffect01Prefab, wj01EffectPos.position, wj01EffectPos.rotation);
        Transform effect01Scale = effect01.GetComponent<Transform>();
        if (effect01Scale != null)
        {
            effect01Scale.localScale = new Vector3(0.45f, 0.75f, 1f);
        }
        StartCoroutine(DestroyEffect(effect01));
    }

    private void ActiveByakuyaWJEffectskill02()
    {
        GameObject effect02 = Instantiate(wjSkillEffect02Prefab, wj02EffectPos.position, wj02EffectPos.rotation);
        Transform effect02Scale = effect02.GetComponent<Transform>();
        if (effect02Scale != null)
        {
            effect02Scale.localScale = new Vector3(0.7f, 0.7f, 1f);
        }
        StartCoroutine(DestroyEffect(effect02));
    }

    private void ActiveByakuyaWJEffectskill03()
    {
        GameObject effect03 = Instantiate(wjSkillEffect03Prefab, wj03EffectPos.position, wj03EffectPos.rotation);
        Transform effect03Scale = effect03.GetComponent<Transform>();
        if (effect03Scale != null)
        {
            effect03Scale.localScale = new Vector3(0.7f, 0.6f, 1f);
        }
    }

    private IEnumerator DestroyEffect(GameObject effect)
    {
        SpriteRenderer effectSprite = effect.GetComponent<SpriteRenderer>();
        if (effectSprite == null)
        {
            yield break;
        }

        Color color = effectSprite.color;

        while (color.a > 0.01f)
        {
            color.a -= Time.deltaTime * 3f;
            effectSprite.color = color;
            yield return null;
        }
        Destroy(effect);
    }

    private void ActiveByakuyaWUSkill()
    {
        StartCoroutine(WUskillEnum());
    }

    private IEnumerator WUskillEnum()
    {
        rb.gravityScale = 0;
        CalWUskillVeloc();
        effectAfterImage.StartAfterImageEffect();
        yield return new WaitForSeconds(0.75f);
        rb.velocity = Vector2.zero;
        Debug.Log(rb.velocity);

        while (playerState.isUsingSkill)
        {
            yield return null;
        }
        rb.gravityScale = originalGravity;
        effectAfterImage.StopAfterImageEffect();
    }

    private void CalWUskillVeloc()
    {
        Vector2 movement;
        int directionAngle = playerState.isFacingRight ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (45f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }

    private void ActiveByakuyaWISkill()
    {
        GameObject skill = Instantiate(wiSkillPrefab, wiSkillPos.position, wiSkillPos.transform.localRotation); 
        SkillCheckHitUseOverLap skillCheck = skill.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
        //anim.speed = 0f;
    }
}
