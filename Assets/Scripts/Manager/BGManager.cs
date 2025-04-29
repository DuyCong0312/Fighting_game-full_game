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
    [SerializeField] private Transform leftBoundaryMap;
    [SerializeField] private Transform rightBoundaryMap;
    [SerializeField] private Transform player01;
    [SerializeField] private Transform player02;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        MoveBG();
    }

    private void MoveBG() 
    {
        float xMiddle = (player01.transform.position.x + player02.transform.position.x) / 2;
        float yMiddle = ((player01.transform.position.y + player02.transform.position.y) / 2) + yOffset;

        float yFinal = (yMiddle > maxY) ? maxY : yMiddle;

        float halfWidth = spriteRenderer.bounds.extents.x;
        float clampedX = Mathf.Clamp(xMiddle, leftBoundaryMap.position.x + halfWidth, rightBoundaryMap.position.x - halfWidth);

        this.transform.localPosition = new Vector3(clampedX, yFinal, transform.position.z);
    }
}
