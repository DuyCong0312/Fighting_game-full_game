using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : MonoBehaviour
{


    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 5f;
    [SerializeField] private float zoomLimiter = 2f;
    [SerializeField] private float maxHeightOffset = 3f;
    [SerializeField] private float minHeightOffset = 1f;
    [SerializeField] private Transform player01;
    [SerializeField] private Transform player02;
    private float leftBoundaryMap;
    private float rightBoundaryMap;

    [SerializeField] private BGManager bgMap;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        StartCoroutine(WaitForBorder());
    }

    private IEnumerator WaitForBorder()
    {
        while (!bgMap.isReady)
        {
            yield return null;
        }
        leftBoundaryMap = bgMap.leftBoundary;
        rightBoundaryMap = bgMap.rightBoundary;
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 midpoint = (player01.position + player02.position) / 2f;
        float distance = Vector3.Distance(player01.position, player02.position);

        float newZoom = Mathf.Clamp(minZoom + distance / zoomLimiter, minZoom, maxZoom);
        cam.orthographicSize = newZoom;

        float heightAdjust = Mathf.Lerp(minHeightOffset, maxHeightOffset, (cam.orthographicSize - minZoom) / (maxZoom - minZoom));
        Vector3 targetPosition = new Vector3(midpoint.x, midpoint.y + heightAdjust, transform.position.z);

        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = cam.orthographicSize * cam.aspect;
        float clampedX = Mathf.Clamp(targetPosition.x, leftBoundaryMap + camHalfWidth, rightBoundaryMap - camHalfWidth);

        float lowestPlayerY = Mathf.Min(player01.position.y, player02.position.y);
        float maxCameraY = lowestPlayerY + camHalfHeight - 0.7f;
        float clampedY = Mathf.Min(targetPosition.y, maxCameraY);

        transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
    }
}
