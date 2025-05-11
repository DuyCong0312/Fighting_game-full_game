using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameModeSO[] gameMode;
    [SerializeField] private GameModeHolderSO gameModeHolder;
    [SerializeField] private GameObject gameModePanel;

    private void Start()
    {
        gameModePanel.SetActive(false);
    }
    public void SelectGameMode(int modeIndex)
    {
        if (modeIndex >= 0 && modeIndex < gameMode.Length)
        {
            gameModeHolder.SetSelectedGameMode(gameMode[modeIndex]);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
