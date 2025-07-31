using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseFighter : MonoBehaviour
{
    [SerializeField] private GameModeHolderSO gameModeHolder;
    [SerializeField] private RectTransform fighterPanelTransform;
    [SerializeField] private RectTransform mapPanelTransform;
    [SerializeField] private FighterSelection fighterSelect;
    [SerializeField] private MapSelection mapSelect; 

    private bool hasStartedTransition = false; 
    private bool hasLoadedScene = false;

    private void Update()
    {
        if (fighterSelect.hasChooseFighter && !hasStartedTransition)
        {
            hasStartedTransition = true;
            StartCoroutine(changeScaleFighterPanel());
        }
        if (mapSelect.hasChooseMap && !hasLoadedScene)
        {
            hasLoadedScene = true;
            LoadGameModeScene();
        }
    }

    private IEnumerator changeScaleMapPanel()
    {
        while (mapPanelTransform.localScale.x < 1f)
        {
            mapPanelTransform.localScale += new Vector3(0.05f, 0.05f, 0f);
            yield return null;
        }
    }

    private IEnumerator changeScaleFighterPanel() 
    {
        Vector3 targetScale = new Vector3(0.1f, 0.1f, 0f);
        while (fighterPanelTransform.localScale.x > targetScale.x)
        {
            fighterPanelTransform.localScale -= new Vector3(0.05f, 0.05f, 0f);
            yield return null;
        }

        fighterPanelTransform.gameObject.SetActive(false);
        mapPanelTransform.gameObject.SetActive(true);
        yield return StartCoroutine(changeScaleMapPanel());
    }

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
