using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fighter
{
    [SerializeField] private Sprite fighterSprite;
    [SerializeField] private string fighterName;
    [SerializeField] private Sprite fighterAvatar;
    [SerializeField] private Sprite fighterFace;
    [SerializeField] private GameObject fighterPrefab;
    [SerializeField] private GameObject fighterComPrefab;

    public Sprite FighterSprite => fighterSprite;
    public string FighterName => fighterName;
    public Sprite FighterAvatar => fighterAvatar;
    public Sprite FighterFace => fighterFace;
    public GameObject FighterPrefab => fighterPrefab;
    public GameObject FighterComPrefab => fighterComPrefab;
}
