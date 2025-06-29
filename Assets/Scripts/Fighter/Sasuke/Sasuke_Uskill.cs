using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Sasuke_Uskill : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private KnockBack knockBack;
    private PlayerState playerState;
    private SpriteRenderer spriteRenderer;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("U Skill")]
    [SerializeField] private GameObject uSkillEffectPrefab;
    [SerializeField] private Transform uSkillEffectPos;
    [SerializeField] private float uForce;

    [Header("U+K Skill")]
    [SerializeField] private float ukForceY;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        knockBack = GetComponentInParent<KnockBack>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ActiveSasukeUSkillEffect()
    {
        Instantiate(uSkillEffectPrefab, uSkillEffectPos.position, uSkillEffectPos.transform.rotation);
        anim.speed = 0f;
        spriteRenderer.enabled = false;
    }

    private void ActiveSasukeUSkillP1()
    {
        StartCoroutine(SasukeUskillEnu());
    }

    private IEnumerator SasukeUskillEnu()
    {
        ActiveSasukeUSkillEffect();
        yield return new WaitForSeconds(0.25f);
        Transform opponent = knockBack.opponentDirection;
        float direction = opponent.eulerAngles.y == 0f ? 1f : -1f;
        Vector2 frontPosition = new Vector2(opponent.position.x + direction * 0.75f, opponent.position.y);
        Instantiate(uSkillEffectPrefab, new Vector2(frontPosition.x, frontPosition.y - 0.6f), uSkillEffectPos.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        transform.parent.position = frontPosition;
        spriteRenderer.enabled = true;
        anim.speed = 1f;
        if (direction == 1f)
        {
            playerState.isFacingRight = false;
            transform.parent.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            playerState.isFacingRight = true;
            transform.parent.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

    }

    private void ActiveSasukeUSkillP2()
    {
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * 2f, direction * uForce);
    }

    private void ActiveSasukeUKSkill()
    {
        effectAfterImage.StartAfterImageEffect();
        rb.velocity = new Vector2(0f, -ukForceY);
    }
}