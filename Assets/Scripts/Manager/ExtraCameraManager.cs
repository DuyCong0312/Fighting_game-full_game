using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExtraCameraManager : MonoBehaviour
{
    public static ExtraCameraManager Instance { get; private set; }
    [SerializeField] private float moveSpeed;
    [Header("UI Effect")]
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Image uiImage;
    private RectTransform uiTransform;


    private Camera extraCam;
    private Camera mainCamera;
    private bool followMainCamera = true;
    private Coroutine currentMoveCoroutine;
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

    private void Start()
    {
        mainCamera = Camera.main;
        extraCam = GetComponent<Camera>();
        uiTransform = uiPanel.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (followMainCamera)
        {
            this.transform.position = mainCamera.transform.position;
            extraCam.orthographicSize = mainCamera.orthographicSize;
        }
    }

    public void EnableFollowMainCamera()
    {
        followMainCamera = true;
    }

    public void DisableFollowMainCamera()
    {
        followMainCamera = false;
    }

    public void MoveCamera(Vector2 newPosition)
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }
        currentMoveCoroutine = StartCoroutine(Move(newPosition));
    }

    private IEnumerator Move(Vector2 newPosition)
    {
        Vector3 target = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        currentMoveCoroutine = null;
    }

    public bool IsMoving()
    {
        if(currentMoveCoroutine != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeSizeCamera(float newSize)
    {
        StartCoroutine(ChangeSize(newSize, 0.4f));
    }

    private IEnumerator ChangeSize(float newSize, float duration)
    {
        float startSize = extraCam.orthographicSize;
        float time = 0f;

        while (time < duration)
        {
            extraCam.orthographicSize = Mathf.Lerp(startSize, newSize, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        extraCam.orthographicSize = newSize;
    }

    public void CallSpecialAttackEffect(Quaternion fighterRotation, Sprite fighterImage)
    {
        extraCam.depth = 1f;
        mainCamera.depth = 0f;
        uiPanel.SetActive(true);
        uiTransform.rotation = fighterRotation;
        uiImage.sprite = fighterImage;
    }

    public void StopSpecialAttackEffect()
    {
        extraCam.depth = 0f;
        mainCamera.depth = 1f;
        uiPanel.SetActive(false);
    }
}
