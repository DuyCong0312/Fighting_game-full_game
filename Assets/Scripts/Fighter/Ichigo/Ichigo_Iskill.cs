using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_Iskill : MonoBehaviour
{

    [Header("I Skill")]
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private GameObject skillPrefab2;
    [SerializeField] private Transform skillPos;

    private void ActivateIchigoISkill(GameObject skill, Quaternion rotation)
    {
        Instantiate(skill, skillPos.position, rotation);
    }

    private void ActiveIchigoISkill01()
    {
        ActivateIchigoISkill(skillPrefab, transform.rotation);
    }

    private void ActiveIchigoISkill02()
    {
        ActivateIchigoISkill(skillPrefab, Quaternion.Euler(180, 0, 0) * transform.rotation);
    }

    private void ActiveIchigoISkill03()
    {
        ActivateIchigoISkill(skillPrefab2, transform.rotation);
    }

}
