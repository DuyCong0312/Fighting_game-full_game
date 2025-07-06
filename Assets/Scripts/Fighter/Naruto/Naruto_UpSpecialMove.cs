using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_UpSpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private KnockBack knockBack;

    [Header("W+J Skill")]
    [SerializeField] private GameObject wjSkillPrefab01;
    [SerializeField] private Transform wjSkillPos01;
    [SerializeField] private GameObject wjSkillPrefab02;
    [SerializeField] private Transform wjSkillPos02;
    [SerializeField] private float wjSpeed;

    [Header("W+U Skill")]
    [SerializeField] private float wuSpeed;
    private float originalGravity;


    [Header("W+I Skill")]
    [SerializeField] private GameObject wiSkillPrefab01;
    [SerializeField] private GameObject wiSkillPrefab02;
    [SerializeField] private GameObject wiSkillPrefab03;
    [SerializeField] private GameObject wiSkillPrefab04;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        knockBack = GetComponentInParent<KnockBack>();
        originalGravity = rb.gravityScale;
    }

    private void ActiveNarutoWJskillMove()
    {
        rb.velocity = new Vector2(wjSpeed, rb.velocity.y);
    }

    private void ActiveNarutoWJskill()
    {
        StartCoroutine(WjSkillEnum());
    }

    private IEnumerator WjSkillEnum()
    { 
        GameObject skill01 = Instantiate(wjSkillPrefab01, wjSkillPos01.position, wjSkillPos01.rotation);
        SkillCheckHitUseOverLap skillCheck01 = skill01.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck01 != null) 
        { 
            skillCheck01.SetOwner(this.gameObject); 
        }
        yield return new WaitForSeconds(0.1f);
        GameObject skill02 = Instantiate(wjSkillPrefab02, wjSkillPos02.position, wjSkillPos01.rotation); 
        SkillCheckHitUseOverLap skillCheck02 = skill02.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck02 != null)
        {
            skillCheck02.SetOwner(this.gameObject);
        }
    }

    private void ActiveNarutoWUskill()
    {
        StartCoroutine(WUskillEnum());
    }

    private IEnumerator WUskillEnum()
    {
        rb.gravityScale = 0f;
        yield return null;
        CalVelocityWUskill();
        while (playerState.isUsingSkill)
        {
            yield return null;
        }
        rb.gravityScale = originalGravity;
    }

    private void CalVelocityWUskill()
    {
        Vector2 movement;
        int directionAngle = playerState.isFacingRight ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (45f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * wuSpeed;
    }
    private void ActiveNarutoWIskill()
    {
        Transform opponent = knockBack.opponentDirection;
        float direction = opponent.eulerAngles.y == 0f ? 1f : -1f;

        Vector2 pos1 = new Vector2(opponent.position.x - 3f * direction, opponent.position.y);
        Vector2 pos2 = new Vector2(opponent.position.x + 3f * direction, opponent.position.y);
        Vector2 pos3 = new Vector2(opponent.position.x - 2f * direction, (opponent.position.y + 2.5f));
        Vector2 pos4 = new Vector2(opponent.position.x + 2f * direction, (opponent.position.y + 2.5f));

        Quaternion rot1 = opponent.rotation * Quaternion.Euler(0, 0, 0);
        //Quaternion rot2 = opponent.rotation * Quaternion.Euler(0, 180, 0); 
        Quaternion rot2 = Quaternion.Euler(0, opponent.rotation.eulerAngles.y == 0f ? 180 : 0, 0);

        GameObject obj1 = Instantiate(wiSkillPrefab01, pos3, rot1);
        SkillCheckHitUseOverLap skillCheckObj01 = obj1.GetComponent<SkillCheckHitUseOverLap>();
        if(skillCheckObj01 != null)
        {
            skillCheckObj01.SetOwner(this.gameObject);
        }
        GameObject obj2 = Instantiate(wiSkillPrefab02, pos4, rot2);
        SkillCheckHitUseOverLap skillCheckObj02 = obj2.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheckObj02 != null)
        {
            skillCheckObj02.SetOwner(this.gameObject);
        }
        GameObject obj3 = Instantiate(wiSkillPrefab03, pos1, rot1);
        SkillCheckHitUseOverLap skillCheckObj03 = obj3.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheckObj03 != null)
        {
            skillCheckObj03.SetOwner(this.gameObject);
        }
        GameObject obj4 = Instantiate(wiSkillPrefab04, pos2, rot2);
        SkillCheckHitUseOverLap skillCheckObj04 = obj4.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheckObj04 != null)
        {
            skillCheckObj04.SetOwner(this.gameObject);
        }
        //for (int i = 0; i < 3; i++)
        //{
        //}
    }
}
