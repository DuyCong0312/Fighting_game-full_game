using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSetting : MonoBehaviour
{
    [Header("References")]
    public GameSettingSO gameSetRuntime;
    public Button applyButton;
    public Button cancelButton;

    [Header("Health")]
    private float[] healthLevels = { 100, 300, 500 };
    private int currentHealthIndex = 0;
    public TextMeshProUGUI healthText;
    public Button increaseHealthButton;
    public Button decreaseHealthButton;
    private float tempHealth;
    private float originalHealth;

    [Header("Time")]
    private float[] timeLevels = { 60, 90, Mathf.Infinity };
    private int currentTimeIndex = 0;
    public TextMeshProUGUI timeText;
    public Button increaseTimeButton;
    public Button decreaseTimeButton;
    public RectTransform rectTransformTime;
    private float tempTime;
    private float originalTime;
    

    private void Start()
    {
        originalHealth = gameSetRuntime.playerHealth;
        tempHealth = originalHealth;

        originalTime = gameSetRuntime.timePerRound;
        tempTime = originalTime;

        currentHealthIndex = System.Array.IndexOf(healthLevels, (int)tempHealth);
        if (currentHealthIndex == -1) currentHealthIndex = 0;

        currentTimeIndex = System.Array.IndexOf(timeLevels, (int)tempTime);
        if (currentTimeIndex == -1) currentTimeIndex = 0;

        increaseHealthButton.onClick.AddListener(IncreaseHealth);
        decreaseHealthButton.onClick.AddListener(DecreaseHealth);
        increaseTimeButton.onClick.AddListener(IncreaseTime);
        decreaseTimeButton.onClick.AddListener(DecreaseTime);
        applyButton.onClick.AddListener(ApplyChanges);
        cancelButton.onClick.AddListener(CancelChanges);

        UpdateHealthText();
        UpdateTimeText();
    }

    private void IncreaseHealth()
    {
        currentHealthIndex++;
        if (currentHealthIndex >= healthLevels.Length)
        {
            currentHealthIndex = 0;
        }
        tempHealth = healthLevels[currentHealthIndex];
        UpdateHealthText();
    }

    private void DecreaseHealth()
    {            
        currentHealthIndex--;
        if (currentHealthIndex < 0)
        {
            currentHealthIndex = healthLevels.Length - 1;
        }
        tempHealth = healthLevels[currentHealthIndex];
        UpdateHealthText();
    }

    private void IncreaseTime()
    {
        currentTimeIndex++;
        if (currentTimeIndex >= timeLevels.Length)
        {
            currentTimeIndex = 0;
        }
        tempTime = timeLevels[currentTimeIndex];
        UpdateTimeText();
    }

    private void DecreaseTime()
    {
        currentTimeIndex--;
        if (currentTimeIndex < 0)
        {
            currentTimeIndex = timeLevels.Length - 1;
        }
        tempTime = timeLevels[currentTimeIndex];
        UpdateTimeText();
    }

    private void ApplyChanges()
    {
        gameSetRuntime.playerHealth = tempHealth;
        gameSetRuntime.timePerRound = tempTime;
    }

    private void CancelChanges()
    {
        tempHealth = originalHealth;
        tempTime = originalTime;
    }

    private void UpdateHealthText()
    {
        healthText.text = tempHealth + "%";
    }
    private void UpdateTimeText()
    {
        if (currentTimeIndex == timeLevels.Length - 1)
        {
            timeText.text = "<size=200%>∞</size>";
        }
        else
        {
            timeText.text = tempTime + "S";
        }
    }
}