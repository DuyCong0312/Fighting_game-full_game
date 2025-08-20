using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byakuya_DefenseSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;
    private PlayerStateMachine player;

    [Header("S+J Skill")]
    [SerializeField] private float force;
    [SerializeField] private GameObject sjSkillEffect01Prefab;
    [SerializeField] private Transform sjEffect01Pos;
    [SerializeField] private GameObject sjSkillEffect02Prefab;
    [SerializeField] private Transform sjEffect02Pos;
    private float originalGravity;

    [Header("S+U Skill")]
    [SerializeField] private GameObject suSkillPrefab;
    [SerializeField] private Transform suSkillPos;

    [Header("S+I Skill")]
    [SerializeField] private float SIforce;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
        player = GetComponentInParent<PlayerStateMachine>();
        originalGravity = rb.gravityScale;
    }

    private void ActiveByakuyaSJskill()
    {
        StartCoroutine(SJskillMove());
    }

    private IEnumerator SJskillMove()
    {
        float direction = playerState.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * force, rb.velocity.y);
        rb.gravityScale = 0;
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.groundDash, player.dashPos, Quaternion.Euler(0, 180, 0) * player.transform.rotation);
        effectAfterImage.StartAfterImageEffect();
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        rb.gravityScale = originalGravity;
        effectAfterImage.StopAfterImageEffect();
    }

    private void ActiveByakuyaSJEffect01skill()
    {
        GameObject effect01 = Instantiate(sjSkillEffect01Prefab, sjEffect01Pos.position, sjEffect01Pos.transform.rotation);
        Transform effect01Scale = effect01.GetComponent<Transform>();
        if (effect01Scale != null) 
        {
            effect01Scale.localScale = new Vector3(1f, 0.6f, 1f);
        }
        StartCoroutine(DestroyByakuyaEffect(effect01));
    }

    private void ActiveByakuyaSJEffect02skill()
    {
        GameObject effect02 = Instantiate(sjSkillEffect02Prefab, sjEffect02Pos.position, sjEffect02Pos.transform.rotation);
        Transform effect02Scale = effect02.GetComponent<Transform>();
        if (effect02Scale != null)
        {
            effect02Scale.localScale = new Vector3(0.6f, 0.5f, 1f);
        }
    }

    private IEnumerator DestroyByakuyaEffect(GameObject effect)
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

    private void ActiveByakuyaSUSkill()
    {
        Instantiate(suSkillPrefab, suSkillPos.position, this.transform.rotation);
    }
    
    private void ActiveByakuyaSISkill()
    {
        rb.velocity = this.transform.right * SIforce;
    }

    private void ActiveByakuyaSIskill02()
    {
        StartCoroutine(SIskillEnuP2());
    }

    private IEnumerator SIskillEnuP2()
    {
        this.gameObject.layer = player.dashLayer;
        float direction = playerState.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * SIforce, 0f);
        yield return new WaitForSeconds(0.15f);
        this.gameObject.layer = player.defaultLayer;
        rb.gravityScale = player.originalGravity;
        rb.velocity = Vector2.zero;
    }
}
