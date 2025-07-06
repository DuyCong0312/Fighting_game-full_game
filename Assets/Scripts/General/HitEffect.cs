using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private Transform hitPos;
    public enum HitEffectType { NormalHit, SlashHit }

    public void HitEffectSpawn(HitEffectType type)
    {
        switch (type)
        {
            case HitEffectType.NormalHit:
                SpawnNormalHit();
                break;
            case HitEffectType.SlashHit:
                SpawnSlashHit();
                break;
        }
    }

    public void SpawnNormalHit()
    {
        EffectManager.Instance.SpawnEffectUseTransform(EffectManager.Instance.hit, hitPos, transform.rotation);
    }

    public void SpawnSlashHit()
    {
        float randomAngle = Random.Range(40f, 145f);

        EffectManager.Instance.SpawnEffectUseTransform(EffectManager.Instance.slashHit01, hitPos, transform.rotation * Quaternion.Euler(0, 0, randomAngle));
        EffectManager.Instance.SpawnEffectUseTransform(EffectManager.Instance.slashHit02, hitPos, transform.rotation * Quaternion.Euler(0, 0, randomAngle));
    }
}
