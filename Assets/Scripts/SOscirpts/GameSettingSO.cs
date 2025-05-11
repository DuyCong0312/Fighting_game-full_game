using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Object/GameSettings")]
public class GameSettingSO : ScriptableObject
{
    public float playerHealth;
    public float timePerRound;

}
