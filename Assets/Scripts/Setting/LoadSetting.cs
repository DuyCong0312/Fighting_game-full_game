using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSetting : MonoBehaviour
{
    [SerializeField] private GameSettingSO gameSetRuntime;
    [SerializeField] private PlayerInputSO player1Input;
    [SerializeField] private PlayerInputSO player2Input;

    private void Awake()
    {
        LoadGameSetting();
        LoadPlayerInput(player1Input, 0);
        LoadPlayerInput(player2Input, 1);
    }

    private void LoadGameSetting()
    {
        gameSetRuntime.playerHealth = PlayerPrefs.GetFloat(CONSTANT.PlayerHealth, gameSetRuntime.playerHealth);
        gameSetRuntime.timePerRound = PlayerPrefs.GetFloat(CONSTANT.TimePerRound, gameSetRuntime.timePerRound);
    }

    private void LoadPlayerInput(PlayerInputSO player, int playerNumber)
    {
        if (player == null) return;

        if (playerNumber == 0)
        {
            player.attack = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P1_Attack, (int)player.attack);
            player.dash = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P1_Dash, (int)player.dash);
            player.defense = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P1_Down, (int)player.defense);
            player.jump = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P1_Jump, (int)player.jump);
            player.moveLeft = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P1_Left, (int)player.moveLeft);
            player.moveRight = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P1_Right, (int)player.moveRight);
            player.rangedAttack = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P1_Skill, (int)player.rangedAttack);
            player.specialAttack = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P1_SpSkill, (int)player.specialAttack);
            player.specialMoveUpInput = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P1_Up, (int)player.specialMoveUpInput);
        }
        else
        {
            player.attack = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P2_Attack, (int)player.attack);
            player.dash = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P2_Dash, (int)player.dash);
            player.defense = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P2_Down, (int)player.defense);
            player.jump = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P2_Jump, (int)player.jump);
            player.moveLeft = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P2_Left, (int)player.moveLeft);
            player.moveRight = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P2_Right, (int)player.moveRight);
            player.rangedAttack = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P2_Skill, (int)player.rangedAttack);
            player.specialAttack = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P2_SpSkill, (int)player.specialAttack);
            player.specialMoveUpInput = (KeyCode)PlayerPrefs.GetInt(CONSTANT.P2_Up, (int)player.specialMoveUpInput);
        }
    }
}
