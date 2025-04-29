using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_Uskill : U_Skill
{
    [Header("U Skill")]
    [SerializeField] private GameObject uSkillPrefab;
    [SerializeField] private Transform uSkillPos;

    [Header("U+K Skill")]
    [SerializeField] private GameObject uKSkillPrefab;
    [SerializeField] private Transform uKSkillPos;

    private void ActiveIchigoUSkill()
    {
        Instantiate(uSkillPrefab, uSkillPos.position, uSkillPos.transform.rotation);
    }
    private void ActiveIchigoUKSkill()
    {
        Instantiate(uKSkillPrefab, uKSkillPos.position, uKSkillPos.transform.rotation);
    }
}
