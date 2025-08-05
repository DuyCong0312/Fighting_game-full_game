using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byakuya_Iskill : MonoBehaviour
{
    private KnockBack knockBack;

    [Header("I Skill")]
    [SerializeField] private GameObject iSkillPrefab;

    private void Start()
    {
        knockBack = GetComponentInParent<KnockBack>();
    }

    private void ActivateByakuyaISkill()
    {
        GameObject skill = Instantiate(iSkillPrefab, knockBack.opponentDirection.position, Quaternion.identity);
        SkillCheckHitUseOverLap skillCheck = skill.GetComponent<SkillCheckHitUseOverLap>();
        if (skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }
}
