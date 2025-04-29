using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffectAfterImage : MonoBehaviour
{
    [SerializeField] private float spawnDelayTime;
    [SerializeField] private GameObject afterImageEffect;
    private Coroutine effectCoroutine;

    public void StartAfterImageEffect()
    {
        if (effectCoroutine == null)
        {
            effectCoroutine = StartCoroutine(AfterImageEffectCoroutine());
        }
    }

    public void StopAfterImageEffect()
    {
        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
            effectCoroutine = null;
        }
    }

    private IEnumerator AfterImageEffectCoroutine()
    {
        while (true)
        {
            GameObject currentAfterImage = Instantiate(afterImageEffect, this.transform.position, this.transform.rotation);
            Sprite currentSprite = GetComponentInChildren<SpriteRenderer>().sprite;
            currentAfterImage.GetComponent<SpriteRenderer>().sprite = currentSprite;
            yield return new WaitForSeconds(spawnDelayTime);
        }
    }
}
