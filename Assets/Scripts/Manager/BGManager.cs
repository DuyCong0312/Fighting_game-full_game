using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    private Transform[] playerTransforms;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float yOffset = 4.0f;
    [SerializeField] private float maxY = 4.5f;
    [SerializeField] private float minY = 2.5f;
    [SerializeField] private Transform leftBoundaryMap;
    [SerializeField] private Transform rightBoundaryMap;
    [SerializeField] private Transform player01;
    [SerializeField] private Transform player02;
    private float currentY = 0f;
    private Camera cam;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        MoveBG();
    }

    private void MoveBG()
    {
        float xMiddle = (player01.position.x + player02.position.x) / 2f;
        float halfWidth = spriteRenderer.bounds.extents.x;
        float clampedX = Mathf.Clamp(xMiddle, leftBoundaryMap.position.x + halfWidth, rightBoundaryMap.position.x - halfWidth);

        float targetY = cam.transform.position.y + yOffset;

        targetY = Mathf.Clamp(targetY, minY, maxY);

        currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * 25f);

        transform.localPosition = new Vector3(clampedX, currentY, transform.position.z);
    }
}
