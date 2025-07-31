using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Scriptable Object/Map")]
public class MapSO : ScriptableObject
{
    [SerializeField] private string mapName;
    [SerializeField] private Sprite mapSprite;
    [SerializeField] private GameObject mapPrefab;

    public string MapName => mapName;
    public Sprite MapSprite => mapSprite;
    public GameObject MapPrefab => mapPrefab;
}
