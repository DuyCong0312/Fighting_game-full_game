using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_Uskill : MonoBehaviour
{
    private KnockBack knockBack;
    private PlayerState playerState;

    [Header("U Skill")]
    [SerializeField] private GameObject uSkillPrefab;
    [SerializeField] private Transform uSkillPos;

    [Header("U+K Skill")]
    [SerializeField] private GameObject uKSkillPrefab;
    [SerializeField] private Transform uKSkillPos;

    private void Start()
    {
        knockBack = GetComponentInParent<KnockBack>();  
        playerState = GetComponentInParent<PlayerState>();
    }

    private void ActiveKakashiUSkillP1()
    {
        GameObject uSkill = Instantiate(uSkillPrefab, uSkillPos.position, uSkillPos.transform.rotation);
        Projectile skillCheck = uSkill.GetComponent<Projectile>();
        if (skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }

    private void ActiveKakashiUSkillP2()
    {
        StartCoroutine(KakashiUskillEnu());
    }

    private IEnumerator KakashiUskillEnu()
    {
        yield return null;
        Transform opponent = knockBack.opponentDirection;
        float direction = opponent.eulerAngles.y == 0f ? -1f : 1f;
        Vector2 behindPosition = new Vector2(opponent.position.x + direction * 1.5f, opponent.position.y);
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

    private void ActiveKakashiUKSkill()
    {
        GameObject ukSkill = Instantiate(uKSkillPrefab, uKSkillPos.position, uKSkillPos.transform.rotation);
        Projectile skillCheck = ukSkill.GetComponent<Projectile>();
        if(skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }
}
