using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_SkillSpawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Transform spawnPos;

    private void Spawn()
    {
        Instantiate(spawnPrefab, spawnPos.position, spawnPos.rotation);
    }
}
