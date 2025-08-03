using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public enum HitEffectType { NormalHit, SlashHit }
    private PlayerState playerState;

    private void Start()
    {
        playerState = GetComponentInParent<PlayerState>();
    }

    public void HitEffectSpawn(HitEffectType type, Vector2 spawnPos)
    {
        if (playerState.isDefending)
        {
            SpawnDefenseHit();
            return;
        }

        switch (type)
        {
            case HitEffectType.NormalHit:
                SpawnNormalHit(spawnPos);
                break;
            case HitEffectType.SlashHit:
                SpawnSlashHit(spawnPos);
                break;
        }
    }

    private void SpawnNormalHit(Vector2 spawnPos)
    {
        EffectManager.Instance.SpawnEffectUseTransform(EffectManager.Instance.hit, spawnPos, transform.rotation);
    }

    private void SpawnSlashHit(Vector2 spawnPos)
    {
        float randomAngle = Random.Range(40f, 145f);

        EffectManager.Instance.SpawnEffectUseTransform(EffectManager.Instance.slashHit01, spawnPos, transform.rotation * Quaternion.Euler(0, 0, randomAngle));
        EffectManager.Instance.SpawnEffectUseTransform(EffectManager.Instance.slashHit02, spawnPos, transform.rotation * Quaternion.Euler(0, 0, randomAngle));
    }

    private void SpawnDefenseHit()
    {
        EffectManager.Instance.SpawnEffectUseTransform(EffectManager.Instance.defenseHit, this.transform.position, transform.rotation);
    }
}
