using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Sakura_Iskill : MonoBehaviour
{
    private Rigidbody2D rb;
    private KnockBack knockBack;
    private PlayerStateMachine player;
    private PlayerState playerState;

    [Header("I Skill")]
    [SerializeField] private float moveSpeed;

    [Header("I+K Skill")]
    [SerializeField] private float force;
    [SerializeField] private GameObject effectIK1;
    [SerializeField] private GameObject effectIK2;
    [SerializeField] private Transform effectIK01Pos;
    [SerializeField] private Transform effectIK02Pos;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        knockBack = GetComponentInParent<KnockBack>();
        playerState = GetComponentInParent<PlayerState>();
        player = GetComponentInParent<PlayerStateMachine>();
    }

    private void ActiveMove()
    {
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.groundDash, player.dashPos, Quaternion.Euler(0, 180, 0) * player.transform.rotation);
        float direction = playerState.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    private void ActiveSakuraIKSkill()
    {
        this.transform.rotation = this.transform.rotation; 
        rb.velocity = new Vector2 (0f, - force);
    }

    private void ActiveEffectIK1()
    {
        SpawnSkillEffect(effectIK1, effectIK01Pos);
    }

    private void ActiveEffectIK2()
    {
        SpawnSkillEffect(effectIK2, effectIK02Pos);
    }
    private void SpawnSkillEffect(GameObject name, Transform nameTransform)
    {
        Instantiate(name, nameTransform.position, nameTransform.rotation, nameTransform);
    }
}
