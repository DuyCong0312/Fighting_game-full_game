using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialMove", menuName = "Scriptable Object/SpecialMove")]
public class SpecialMoveSO : ScriptableObject
{
    public string moveName;
    public string animationName;
    public float rageCost;
}
