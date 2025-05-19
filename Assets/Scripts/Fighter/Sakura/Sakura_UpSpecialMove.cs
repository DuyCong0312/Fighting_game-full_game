using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_UpSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private KnockBack knockBack;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("W+J Skill")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float reboundForce;

    [Header("W+U Skill")]
    [SerializeField] private GameObject wuSkillPrefab;
    [SerializeField] private Transform wuSkillPos;

    [Header("W+I Skill")]
    [SerializeField] private float force;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        knockBack = GetComponentInParent<KnockBack>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
    }

    private void ActiveSakuraWJSkillP1()
    {
        rb.velocity = this.transform.right * moveSpeed;
    }

    private void ActiveSakuraWJSkillP2()
    {
        rb.velocity = new Vector2 (rb.velocity.x, reboundForce);
    }

    private void StopSakuraWJSkill()
    {
        rb.velocity = Vector2.zero;
    }

    private void ActiveSakuraWUSkill()
    {
        SakuraWUSkill();
    }

    private void SakuraWUSkill()
    {
        Instantiate(wuSkillPrefab, wuSkillPos.position, wuSkillPos.transform.rotation);
        Instantiate(wuSkillPrefab, wuSkillPos.position, wuSkillPos.transform.rotation * Quaternion.Euler(0, 0, 20));
        Instantiate(wuSkillPrefab, wuSkillPos.position, wuSkillPos.transform.rotation * Quaternion.Euler(0, 0, -20));
    }

    private void ActiveSakuraWISkill()
    {
        StartCoroutine(SakuraWISkill());
    }

    private IEnumerator SakuraWISkill()
    {
        effectAfterImage.StartAfterImageEffect();
        rb.velocity = this.transform.right * force;
        yield return new WaitForSeconds(0.1f);
        transform.parent.position = new Vector2 (knockBack.opponentDirection.position.x, knockBack.opponentDirection.position.y + 5f);
        rb.velocity = Vector2.zero;
        anim.Play(CONSTANT.IKskill);
        effectAfterImage.StopAfterImageEffect();
    }
}
