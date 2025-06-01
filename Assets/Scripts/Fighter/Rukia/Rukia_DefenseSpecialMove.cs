using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Rukia_DefenseSpecialMove : MonoBehaviour
{
    [Header("S+J")]
    [SerializeField] private Transform SJskillPos;
    private KnockBack knockBack;
    private PlayerState playerState;
    //[Header("S+U")] Ben CheckHit
    [Header("S+I")]
    [SerializeField] private Transform SIskillPos;
    [SerializeField] private GameObject SIskill1;
    [SerializeField] private GameObject SIskill2;

    private void Start()
    {
        playerState = GetComponentInParent<PlayerState>();
        knockBack = GetComponentInParent<KnockBack>();
    }

    private void ActiveRukiaSJskill()
    {
        StartCoroutine(RukiaSJskillEnu());
    }

    private IEnumerator RukiaSJskillEnu()
    {
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.groundDash, SJskillPos.position, Quaternion.Euler(0, 180, 0) * transform.rotation);
        yield return null;
        Transform opponent = knockBack.opponentDirection;
        float direction = opponent.eulerAngles.y == 0f ? -1f : 1f;
        Vector2 behindPosition = new Vector2(opponent.position.x + direction * 1f, opponent.position.y);
        transform.parent.position = behindPosition;
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

    private void ActiveRukiaSI_1skill()
    {
        GameObject skill = Instantiate(SIskill1, SIskillPos.position, SIskillPos.rotation);
        SkillCheckHit skillCheck = skill.GetComponent<SkillCheckHit>();
        if (skillCheck != null)
            skillCheck.SetOwner(this.gameObject);
    }
    private void ActiveRukiaSI_2skill()
    {
        Instantiate(SIskill2, SIskillPos.position, SIskillPos.rotation);
    }
}
