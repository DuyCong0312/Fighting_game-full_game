using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopController : MonoBehaviour
{
    public static HitStopController Instance { get; private set; }
    [SerializeField] private float duration = 0.05f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void HitStop()
    {
        StartCoroutine(DoHitStop());
    }

    private IEnumerator DoHitStop()
    {
        float originalTimeScale = Time.timeScale;
        yield return new WaitForSecondsRealtime(0.025f);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = originalTimeScale;
    }
}

