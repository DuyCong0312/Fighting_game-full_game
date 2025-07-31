using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeHolder", menuName = "Scriptable Object/GameModeHolder")]
public class GameModeHolderSO : ScriptableObject
{
    public GameModeSO selectedGameMode;

    public void SetSelectedGameMode(GameModeSO mode)
    {
        selectedGameMode = mode;
    }

    public GameModeSO GetSelectedGameMode()
    {
        return selectedGameMode;
    }

    public bool IsPVsC()
    {
        if(selectedGameMode != null && selectedGameMode.gameModeName == CONSTANT.Player_vs_Computer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsPVsP()
    {
        if (selectedGameMode != null && selectedGameMode.gameModeName == CONSTANT.Player_vs_Player)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsTraining()
    {
        if (selectedGameMode != null && selectedGameMode.gameModeName == CONSTANT.Training)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
