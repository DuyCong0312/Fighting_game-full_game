using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fighter", menuName = "Scriptable Object/Fighter")]
public class FighterSO : ScriptableObject
{
    [SerializeField] private string fighterName;
    [SerializeField] private Sprite fighterSprite;
    [SerializeField] private Sprite fighterAvatar;
    [SerializeField] private Sprite fighterFace;
    [SerializeField] private GameObject fighterPrefab;

    public string FighterName => fighterName;
    public Sprite FighterSprite => fighterSprite;
    public Sprite FighterAvatar => fighterAvatar;
    public Sprite FighterFace => fighterFace;
    public GameObject FighterPrefab => fighterPrefab;
}
