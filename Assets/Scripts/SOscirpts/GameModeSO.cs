using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "Scriptable Object/GameMode")]
public class GameModeSO : ScriptableObject
{
    public string gameModeName;
    public string sceneName;
}
