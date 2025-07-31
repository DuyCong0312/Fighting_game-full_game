using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    [SerializeField] private List<MapSO> mapList = new List<MapSO>();
    [SerializeField] private Image MapImage;
    [SerializeField] private Button chooseMapButton;
    [SerializeField] private Button increaseMapButton;
    [SerializeField] private Button decreaseMapButton;

    private int selectedMapIndex = 0;
    public bool hasChooseMap = false;

    private void Start()
    {
        hasChooseMap = false;
        chooseMapButton.onClick.AddListener(SaveChoices);
        increaseMapButton.onClick.AddListener(IncreaseMap);
        decreaseMapButton.onClick.AddListener(DecreaseMap);

        UpdateMapImage();
    }

    private void IncreaseMap()
    {
        selectedMapIndex++;
        if (selectedMapIndex >= mapList.Count)
        {
            selectedMapIndex = 0;
        }
        UpdateMapImage();
    }

    private void DecreaseMap()
    {
        selectedMapIndex--;
        if (selectedMapIndex < 0)
        {
            selectedMapIndex = mapList.Count - 1;
        }
        UpdateMapImage();
    }

    private void SaveChoices()
    {
        hasChooseMap = true;
        PlayerPrefs.SetInt(CONSTANT.SelectedMapIndex, selectedMapIndex);
        PlayerPrefs.Save();
    }

    private void UpdateMapImage()
    {
        MapImage.sprite = mapList[selectedMapIndex].MapSprite;
    }
}
