using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_UpSpecialMove : MonoBehaviour
{
    [Header("W+J Skill")]
    [SerializeField] private GameObject wjSkillPrefab;
    [SerializeField] private Transform wjSkillPos;

    [Header("W+U Skill")]
    [SerializeField] private GameObject wuSkillPrefab;
    [SerializeField] private Transform wuKSkillPos;

    [Header("W+I Skill")]
    [SerializeField] private GameObject wiSkillPrefab;
    [SerializeField] private Transform wiSkillPos;

    private void ActiveIchigoWJSkill()
    {
        GameObject skillObj = Instantiate(wjSkillPrefab, wjSkillPos.position, wjSkillPos.transform.localRotation);
        SkillCheckHit skillCheck = skillObj.GetComponent<SkillCheckHit>();
        if (skillCheck != null) 
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }
    private void ActiveIchigoWUSkill()
    {
        Instantiate(wuSkillPrefab, wuKSkillPos.position, wuKSkillPos.transform.rotation);
    }
    private void ActiveIchigoWISkill()
    {
        Instantiate(wiSkillPrefab, wiSkillPos.position, wiSkillPos.transform.localRotation);
    }
}
