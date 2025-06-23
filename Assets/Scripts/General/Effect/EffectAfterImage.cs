using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAfterImage : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActived;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    private float alphaMultiplier = 0.85f;

    private SpriteRenderer spriteRenderer;

    private Color color;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alpha = alphaSet;
        timeActived = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        spriteRenderer.color = color;

        if(Time.time >= (timeActived + activeTime))
        {
            Destroy(gameObject);
        }
    }
}
