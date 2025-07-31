using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadMap : MonoBehaviour
{
    [SerializeField] private List<MapSO> mapList = new List<MapSO>();
    [SerializeField] private GameObject mapPrefab;


    private void Start()
    {
        LoadSelectedMap(CONSTANT.SelectedMapIndex, mapPrefab);
    }

    private void LoadSelectedMap(string prefsKey, GameObject MapPrefab)
    {
        int selectedMapIndex = PlayerPrefs.GetInt(prefsKey, 0);
        Instantiate(mapList[selectedMapIndex].MapPrefab, MapPrefab.transform.position, MapPrefab.transform.rotation, MapPrefab.transform);
    }
}

