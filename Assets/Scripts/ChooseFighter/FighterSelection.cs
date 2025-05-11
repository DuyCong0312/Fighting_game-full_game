using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FighterSelection : MonoBehaviour
{
    [SerializeField] private List<Fighter> fighterList = new List<Fighter>();
    [SerializeField] private TextMeshProUGUI firstFighterName;
    [SerializeField] private Image firstFighterImage;
    [SerializeField] private TextMeshProUGUI secondFighterName;
    [SerializeField] private Image secondFighterImage;

    [SerializeField] private int selectedFirstFighterIndex = 0;
    [SerializeField] private int selectedSecondFighterIndex = 0;

    private int currentPlayer = 1;

    private void Start()
    {
        if (fighterList.Count > 0)
        {
            ClearPreview(firstFighterImage, firstFighterName);
            ClearPreview(secondFighterImage, secondFighterName);
        }
        else
        {
            Debug.LogWarning("Fighter list is empty!");
        }
    }

    public void ChooseFighter(int fighterIndex)
    {
        if (currentPlayer == 1)
        {
            SetFighterInfo(fighterIndex, firstFighterImage, firstFighterName, ref selectedFirstFighterIndex);
            currentPlayer = 2;
        }
        else if (currentPlayer == 2)
        {
            SetFighterInfo(fighterIndex, secondFighterImage, secondFighterName, ref selectedSecondFighterIndex);
            currentPlayer = 0; 
        }
    }

    public void RandomFighter()
    {
        int randomFighter = Random.Range(0, fighterList.Count);
        if (currentPlayer == 1)
        {
            SetFighterInfo(randomFighter, firstFighterImage, firstFighterName, ref selectedFirstFighterIndex);
            currentPlayer = 2;
        }
        else if (currentPlayer == 2)
        {
            SetFighterInfo(randomFighter, secondFighterImage, secondFighterName, ref selectedSecondFighterIndex);
            currentPlayer = 0;
        }
    }

    private void SetFighterInfo(int fighterIndex, Image fighterImage, TextMeshProUGUI fighterName, ref int selectedFighterIndex)
    {
        fighterImage.sprite = fighterList[fighterIndex].FighterSprite;
        fighterName.text = fighterList[fighterIndex].FighterName;
        selectedFighterIndex = fighterIndex;
        SaveChoices();
    }

    private void SaveChoices()
    {
        PlayerPrefs.SetInt(CONSTANT.SelectedFirstFighterIndex, selectedFirstFighterIndex);
        PlayerPrefs.SetInt(CONSTANT.SelectedSecondFighterIndex, selectedSecondFighterIndex);
        PlayerPrefs.Save();
    }

    public void PreviewFighter(int fighterIndex)
    {
        if (fighterIndex < 0 || fighterIndex >= fighterList.Count)
        {
            Debug.LogWarning("Invalid fighter index for preview!");
            return;
        }

        if (currentPlayer == 1)
        {
            UpdatePreview(firstFighterImage, firstFighterName, fighterIndex);
        }
        else if (currentPlayer == 2)
        {
            UpdatePreview(secondFighterImage, secondFighterName, fighterIndex);
        }
    }

    private void UpdatePreview(Image fighterImage, TextMeshProUGUI fighterName, int fighterIndex)
    {
        fighterImage.color = new Color(1, 1, 1, 1);
        fighterImage.sprite = fighterList[fighterIndex].FighterSprite;
        fighterName.text = fighterList[fighterIndex].FighterName;
    }

    public void ClearPreview()
    {
        if (currentPlayer == 1)
        {
            ClearPreview(firstFighterImage, firstFighterName);
        }
        else if (currentPlayer == 2)
        {
            ClearPreview(secondFighterImage, secondFighterName);
        }
    }

    private void ClearPreview(Image fighterImage, TextMeshProUGUI fighterName)
    {
        fighterImage.color = new Color(1, 1, 1, 0);
        fighterName.text = "";
    }
}
