using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    private SpriteRenderer bgSpriteRenderer;

    [SerializeField] private float yOffset = 7.35f;
    [SerializeField] private float maxY = 4.5f;
    [SerializeField] private float minY = 2.5f;
    [SerializeField] private Transform player01;
    [SerializeField] private Transform player02;
    private float currentY = 0f;
    private Camera cam;

    public float leftBoundary;
    public float rightBoundary;
    public bool isReady = false;

    private void Start()
    {
        StartCoroutine(FindBackGround());
        StartCoroutine(FindGround());

        cam = Camera.main;
    }

    private IEnumerator FindGround()
    {
        GameObject ground = GameObject.FindWithTag("Ground");
        while (ground == null)
        {
            ground = GameObject.FindWithTag("Ground");
            yield return null;
        }
        SpriteRenderer groundRenderer = ground.GetComponent<SpriteRenderer>();
        if (groundRenderer != null)
        {
            Bounds bounds = groundRenderer.bounds;
            leftBoundary = bounds.min.x;
            rightBoundary = bounds.max.x;
        }
        isReady = true;
    }

    private IEnumerator FindBackGround()
    {
        GameObject bg = GameObject.FindWithTag("BackGround");
        while (bg == null)
        {
            bg = GameObject.FindWithTag("BackGround");
            yield return null;
        }
        bgSpriteRenderer = bg.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (isReady && bgSpriteRenderer != null)
        {
            MoveBG();
        }
    }

    private void MoveBG()
    {
        float xMiddle = (player01.position.x + player02.position.x) / 2f;
        float halfWidth = bgSpriteRenderer.bounds.extents.x;
        float clampedX = Mathf.Clamp(xMiddle, leftBoundary + halfWidth, rightBoundary - halfWidth);

        float targetY = cam.transform.position.y + yOffset;
        targetY = Mathf.Clamp(targetY, minY, maxY);
        currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * 25f);

        bgSpriteRenderer.transform.position = new Vector3(clampedX, currentY, bgSpriteRenderer.transform.position.z);
    }
}
