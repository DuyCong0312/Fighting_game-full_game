using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renji_UpSpecialMove : MonoBehaviour
{
    [Header("W+J Skill")]
    [SerializeField] private GameObject WjSkillPrefab;
    [SerializeField] private Transform WjSkillPos;

    private void ActiveRenjiWJSkill()
    {
        GameObject skillObj = Instantiate(WjSkillPrefab, WjSkillPos.position, WjSkillPos.transform.localRotation);
        SkillCheckHit skillCheck = skillObj.GetComponent<SkillCheckHit>();
        if (skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }

}
