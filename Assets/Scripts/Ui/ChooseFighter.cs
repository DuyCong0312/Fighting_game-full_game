using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseFighter : MonoBehaviour
{
    [SerializeField] private GameModeHolderSO gameModeHolder;

    public void LoadGameModeScene()
    {
        string gameModeSceneName = gameModeHolder.GetSelectedGameMode().sceneName;
        ChangeScene(gameModeSceneName);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
