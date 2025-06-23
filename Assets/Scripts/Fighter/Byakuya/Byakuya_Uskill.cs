using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byakuya_Uskill : MonoBehaviour
{
    [Header("U Skill")]
    [SerializeField] private GameObject uSkillPrefab;
    [SerializeField] private Transform uSkillPos;

    [Header("U+K Skill")]
    [SerializeField] private GameObject uKSkillPrefab;
    [SerializeField] private Transform uKSkillPos;

    private void ActiveByakuyaUSkill()
    {
        Instantiate(uSkillPrefab, uSkillPos.position, uSkillPos.transform.rotation);
    }
    private void ActiveByakuyaUKSkill()
    {
        GameObject skill = Instantiate(uKSkillPrefab, uKSkillPos.position, uKSkillPos.transform.rotation);
        Projectile skillCheck = skill.GetComponent<Projectile>();
        if (skillCheck != null)
        {
            skillCheck.SetOwner(this.gameObject);
        }
    }
}
