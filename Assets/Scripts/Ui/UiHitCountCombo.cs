using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiHitCountCombo : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI hitText;
    [SerializeField] private float setTime;
    [SerializeField] private float popScale = 1.5f;       
    [SerializeField] private float popDuration = 0.2f;    
    [SerializeField] private float vanishDuration = 0.3f;

    private float timeToReset;
    private int lastHitCount = 0;
    private Vector3 originalScale;
    private Color originalColor;

    private Coroutine popRoutine;
    private Coroutine vanishRoutine;

    private void Start()
    {
        timeToReset = setTime; 
        originalScale = hitText.transform.localScale;
        originalColor = hitText.color;
        hitText.text = "";
    }
    private void Update()
    {
        if (playerHealth.getHitCount > lastHitCount)
        {
            lastHitCount = playerHealth.getHitCount;
            UpdateHitCountText(lastHitCount);
            timeToReset = setTime; 
            
            if (popRoutine != null) StopCoroutine(popRoutine);
            popRoutine = StartCoroutine(PopEffect());
        }

        if (lastHitCount > 0)
        {
            timeToReset -= Time.deltaTime;

            if (timeToReset <= 0f)
            {
                lastHitCount = 0;
                playerHealth.getHitCount = 0;

                if (vanishRoutine != null) StopCoroutine(vanishRoutine);
                vanishRoutine = StartCoroutine(VanishEffect());
            }
        }
    }

    private void UpdateHitCountText(int count)
    {
        if(count > 1)
        {
            hitText.text = count.ToString() + " " + "hit";
        }
    }

    private IEnumerator PopEffect()
    {
        float t = 0;
        Vector3 startScale = originalScale;
        Vector3 targetScale = originalScale * popScale;

        Color startColor = originalColor;
        Color flashColor = Color.white;

        while (t < 1)
        {
            t += Time.deltaTime / popDuration;
            float curve = Mathf.Sin(t * Mathf.PI);
            hitText.transform.localScale = Vector3.Lerp(startScale, targetScale, curve);
            hitText.color = Color.Lerp(startColor, flashColor, curve);
            yield return null;
        }

        hitText.transform.localScale = originalScale;
        hitText.color = originalColor;
    }

    private IEnumerator VanishEffect()
    {
        float t = 0;
        Vector3 startScale = hitText.transform.localScale;
        Vector3 targetScale = Vector3.zero;
        Color startColor = hitText.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (t < 1)
        {
            t += Time.deltaTime / vanishDuration;
            hitText.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            hitText.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        hitText.text = "";
        hitText.transform.localScale = originalScale;
        hitText.color = originalColor;
    }
}
