using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadFighter : MonoBehaviour
{
    [SerializeField] private GameModeHolderSO gameModeHolder;
    [SerializeField] private List<FighterSO> fighterList = new List<FighterSO>();

    [Header("Player01")]
    [SerializeField] private TextMeshProUGUI firstFighterName;
    [SerializeField] private Image firstFighterAvatar;
    [SerializeField] private Image firstFighterFace;
    [SerializeField] private GameObject firstFighterPrefab;

    [Header("Player02")]
    [SerializeField] private TextMeshProUGUI secondFighterName;
    [SerializeField] private Image secondFighterAvatar;
    [SerializeField] private Image secondFighterFace;
    [SerializeField] private GameObject secondFighterPrefab;

    private void Start()
    {
       CheckGameMode();
    } 

    private void CheckGameMode()
    {
        if (gameModeHolder.IsPVsC())
        {
            LoadSelectedFighter(CONSTANT.SelectedFirstFighterIndex, firstFighterAvatar, firstFighterName, firstFighterFace, firstFighterPrefab);
            LoadSelectedCom(CONSTANT.SelectedSecondFighterIndex, secondFighterAvatar, secondFighterName, secondFighterFace, secondFighterPrefab);
        }
        else if (gameModeHolder.IsPVsP()) 
        {
            LoadSelectedFighter(CONSTANT.SelectedFirstFighterIndex, firstFighterAvatar, firstFighterName, firstFighterFace, firstFighterPrefab);
            LoadSelectedFighter(CONSTANT.SelectedSecondFighterIndex, secondFighterAvatar, secondFighterName, secondFighterFace, secondFighterPrefab);
        }
    }

    private void LoadSelectedCom(string prefsKey, Image fighterAvatar, TextMeshProUGUI fighterName, Image fighterFace, GameObject fighterPrefab)
    {
        int selectedFighterIndex = PlayerPrefs.GetInt(prefsKey, 0);
        fighterAvatar.sprite = fighterList[selectedFighterIndex].FighterAvatar;
        fighterName.text = fighterList[selectedFighterIndex].FighterName;
        fighterFace.sprite = fighterList[selectedFighterIndex].FighterFace;
        Instantiate(fighterList[selectedFighterIndex].FighterComPrefab, fighterPrefab.transform.position, fighterPrefab.transform.rotation, fighterPrefab.transform);
    }

    private void LoadSelectedFighter(string prefsKey, Image fighterAvatar,  TextMeshProUGUI fighterName, Image fighterFace, GameObject fighterPrefab)
    {
        int selectedFighterIndex = PlayerPrefs.GetInt(prefsKey, 0);
        fighterAvatar.sprite = fighterList[selectedFighterIndex].FighterAvatar;
        fighterName.text = fighterList[selectedFighterIndex].FighterName;
        fighterFace.sprite = fighterList[selectedFighterIndex].FighterFace;
        Instantiate(fighterList[selectedFighterIndex].FighterPrefab, fighterPrefab.transform.position, fighterPrefab.transform.rotation, fighterPrefab.transform);
    }
}
