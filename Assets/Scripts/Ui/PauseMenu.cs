using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelPauseGame;
    public void Continue()
    {
        Time.timeScale = 1f;
        GameManager.Instance.gamePause = false;
        panelPauseGame.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        GameManager.Instance.gamePause = false;
        SceneManager.LoadScene(sceneName);
    }
}
