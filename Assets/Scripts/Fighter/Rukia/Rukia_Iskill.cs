using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rukia_Iskill : MonoBehaviour
{
    [Header("I Skill")]
    [SerializeField] private GameObject iSkillPrefab;
    [SerializeField] private Transform iSkillPos;

    [Header("I+K Skill")]
    [SerializeField] private GameObject iKSkillPrefab;
    [SerializeField] private Transform iKSkillPos;

    private void ActiveRukiaISkill()
    {
        Instantiate(iSkillPrefab, iSkillPos.position, iSkillPos.transform.rotation);
    }
    private void ActiveRukiaIKSkill()
    {
        Instantiate(iKSkillPrefab, iKSkillPos.position, iKSkillPos.transform.rotation);
    }
}
