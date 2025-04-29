using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FighterSelection : MonoBehaviour
{
    [SerializeField] private List<Fighter> fighterList = new List<Fighter>();   

    [SerializeField] private TextMeshProUGUI fighterName;
    [SerializeField] private Image fighterImage;
    [SerializeField] private int selectedFighterIndex = 0;

    private void Start()
    {
        if (fighterList.Count > 0)
        {
            RandomFighter();
        }
        else
        {
            Debug.LogWarning("Fighter list is empty!");
        }
    }
    public void ChooseFighter(int fighterIndex)
    {
        fighterImage.sprite = fighterList[fighterIndex].FighterSprite;
        fighterName.text = fighterList[fighterIndex].FighterName;
        selectedFighterIndex = fighterIndex;
        SaveFighter();
    }

    public void RandomFighter()
    {
        int randomFighter = Random.Range(0, fighterList.Count -1);
        fighterImage.sprite = fighterList[fighterList.Count - 1].FighterSprite;
        fighterName.text = fighterList[fighterList.Count - 1].FighterName;
        selectedFighterIndex = randomFighter;
        SaveFighter();
    }
    private void SaveFighter()
    {
        PlayerPrefs.SetInt(CONSTANT.SelectedFighterIndex,selectedFighterIndex);
        PlayerPrefs.Save();
    }
}
