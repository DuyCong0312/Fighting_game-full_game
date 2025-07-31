using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingRoomRule : MonoBehaviour
{
    [Header("Player_1")]
    [SerializeField] private PlayerHealth player01Health;
    [SerializeField] private PlayerRage player01Rage;

    [Header("Player_2")]
    [SerializeField] private PlayerHealth player02Health;
    [SerializeField] private PlayerRage player02Rage;
    private void Start()
    {
        StartCoroutine(TrainingLoop());
    }

    private IEnumerator TrainingLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            player01Health.ResetHealth();
            player02Health.ResetHealth();

            player01Rage.ResetRage();
            player02Rage.ResetRage();
        }
    }
}
