using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "Scriptable Object/PlayerInput")]
public class PlayerInputSO : ScriptableObject
{
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;
    public KeyCode dash;
    public KeyCode attack;
    public KeyCode defense;
    public KeyCode rangedAttack;
    public KeyCode specialAttack;
    public KeyCode specialMoveUpInput;
}
