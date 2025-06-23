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
    [SerializeField] private GameObject sjSkillEffectPrefab;
    [SerializeField] private Transform sjEffectPos;
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

    private void ActiveByakuyaSJEffectskill()
    {
        StartCoroutine(DestroyEffect());
    }
    private IEnumerator DestroyEffect()
    {
        GameObject effect = Instantiate(sjSkillEffectPrefab, sjEffectPos.position, this.transform.rotation);
        yield return new WaitForSeconds(0.1f);
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
}
