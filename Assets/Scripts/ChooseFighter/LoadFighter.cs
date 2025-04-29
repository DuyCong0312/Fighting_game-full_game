using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadFighter : MonoBehaviour
{
    [SerializeField] private List<Fighter> fighterList = new List<Fighter>();

    [SerializeField] private TextMeshProUGUI fighterName;
    [SerializeField] private Image fighterAvatar;
    [SerializeField] private Image fighterFace;
    [SerializeField] private GameObject fighterPrefab;
    private int selectedFighterIndex;

    private void Start()
    {
        selectedFighterIndex = PlayerPrefs.GetInt(CONSTANT.SelectedFighterIndex, 0);
        fighterAvatar.sprite = fighterList[selectedFighterIndex].FighterAvatar;
        fighterName.text = fighterList[selectedFighterIndex].FighterName;
        fighterFace.sprite = fighterList[selectedFighterIndex].FighterFace;
        fighterPrefab = Instantiate(fighterList[selectedFighterIndex].FighterPrefab, fighterPrefab.transform.position, fighterPrefab.transform.rotation, fighterPrefab.transform);
    }
}
