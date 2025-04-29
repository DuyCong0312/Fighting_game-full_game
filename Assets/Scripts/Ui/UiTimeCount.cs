using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTimeCount : MonoBehaviour
{
    [SerializeField] private float timeLeft;
    [SerializeField] private float setTime = 60;
    [SerializeField] private TextMeshProUGUI timeCount;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        timeLeft = setTime;
    }
    void Update()
    {
        if (!GameManager.Instance.gameStart
            || GameManager.Instance.gameEnded)
        {
            return;
        }

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) timeLeft = 0;
            TotalTime(timeLeft);
        }
        else if (timeLeft <= 0)
        {
            timeLeft = 0;
            gameManager.GameSetTime();
        }
    }

    public void ResetTime()
    {
        timeLeft = setTime;
    }

    private void TotalTime(float totalTime)
    {
        timeCount.text = Mathf.FloorToInt(totalTime).ToString();
    }
}
