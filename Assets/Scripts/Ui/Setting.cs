using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [SerializeField] private GameObject playerInputSettingPanel;

    private void Start()
    {
        playerInputSettingPanel.SetActive(false);    
    }

    public void ChangePlayerInput()
    {
        playerInputSettingPanel.SetActive(true);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
