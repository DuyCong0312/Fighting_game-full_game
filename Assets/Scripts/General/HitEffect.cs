using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private Transform hitPos;

    public void SpawnEffect()
    {
        EffectManager.Instance.SpawnEffectUseTransform(EffectManager.Instance.hit, hitPos, transform.rotation);
    }
}
