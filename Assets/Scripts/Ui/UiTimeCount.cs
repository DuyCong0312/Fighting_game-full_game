using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTimeCount : MonoBehaviour
{
    public GameSettingSO gameSetRuntime;
    [SerializeField] private float timeLeft;
    [SerializeField] private float setTime;
    [SerializeField] private TextMeshProUGUI timeCount;
    [SerializeField] private GameManager gameManager;
    private const float InfinityThreshold = 999999f;

    private void Start()
    {
        setTime = gameSetRuntime.timePerRound;
        timeLeft = setTime;
        TotalTime(timeLeft);
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
        TotalTime(timeLeft);
    }

    private void TotalTime(float totalTime)
    { 
        if(totalTime > InfinityThreshold)
        {
            timeCount.text = "<size=200%>∞</size>";
        }
        else
        {
            timeCount.text = Mathf.FloorToInt(totalTime).ToString();
        }
    }
}
