using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int roundNumber = 1;
    [SerializeField] private CameraManager cam; 
    [SerializeField] private GameModeHolderSO gameMode;

    [Header("Player_1")]
    [SerializeField] private PlayerHealth player01Health;
    [SerializeField] private Transform player01Pos;
    [SerializeField] private Toggle[] player1Wins;
    private int player1RoundsWon = 0;

    [Header("Player_2")]
    [SerializeField] private PlayerHealth player02Health;
    [SerializeField] private Transform player02Pos;
    [SerializeField] private Toggle[] player2Wins;
    private int player2RoundsWon = 0;

    [Header("Game UI Settings")]
    [SerializeField] private UiTimeCount time;
    [SerializeField] private GameObject panelGameSetPerRound;
    [SerializeField] private TextMeshProUGUI gameSetPerRound;
    [SerializeField] private GameObject panelGameSetFinal;
    [SerializeField] private TextMeshProUGUI gameSetFinal;
    [SerializeField] private GameObject panelStartGame;
    [SerializeField] private TextMeshProUGUI gameRound;
    [SerializeField] private TextMeshProUGUI startGame;
    [SerializeField] private GameObject mainPanelPauseGame;
    [SerializeField] private GameObject extraPanelPauseGame;

    [SerializeField] private Camera extraCamera;
    private Camera mainCamera;

    private bool currentCameraIsMainCam;

    private Vector2 player01SpawnPoint;
    private Vector2 player02SpawnPoint;

    public bool canMoveExtraCam = true;
    public bool gameStart = false;
    public bool gamePause = false;
    public bool gameEnded = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        player01SpawnPoint = player01Pos.position;
        player02SpawnPoint = player02Pos.position;
        panelGameSetPerRound.SetActive(false);
        panelGameSetFinal.SetActive(false);
        mainPanelPauseGame.SetActive(false);
        extraPanelPauseGame.SetActive(false);
        panelStartGame.SetActive(false);
        StartCoroutine(BeforeStartGame());
    }

    private void Update()
    {
        PauseGame();
        if (gameMode.IsTraining())
        {
            return;
        }
        if (!gameEnded && (player01Health.currentHealth <= 0 || player02Health.currentHealth <= 0))
        {
            GameSet(); 
        }
    }
    public void GameSetTime()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (player01Health.currentHealth > player02Health.currentHealth)
        {
            
            gameSetPerRound.text = ("You win");
            AwardWinToPlayer(1);
        }
        else if(player01Health.currentHealth < player02Health.currentHealth)
        {
            gameSetPerRound.text = ("You lose");
            AwardWinToPlayer(2);
        }
        else 
        {
            gameSetPerRound.text = ("Draw");
            SetPanel();
            StartCoroutine(StartNewRound());
        }
    }

    private void GameSet()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (player01Health.currentHealth <= 0)
        {
            gameSetPerRound.text = ("You lose");
            AwardWinToPlayer(2);
        }
        else if (player02Health.currentHealth <= 0)
        {
            gameSetPerRound.text = ("You win");
            AwardWinToPlayer(1);
        }
        else if (player01Health.currentHealth <= 0 && player02Health.currentHealth <= 0)
        {
            gameSetPerRound.text = "Draw";
            SetPanel();
            StartCoroutine(StartNewRound());
        }
    }

    private void SetPanel()
    {
        panelGameSetPerRound.SetActive(true);
    }

    private void AwardWinToPlayer(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Wins[player1RoundsWon].isOn = true;
            player1RoundsWon++;
        }
        else
        {
            player2Wins[player2RoundsWon].isOn = true;
            player2RoundsWon++;
        }

        if (CheckFinalWin())
        {
            return;
        }
        else
        {
            panelGameSetPerRound.SetActive(true);
        }

        StartCoroutine(StartNewRound());
    }

    private bool CheckFinalWin()
    {
        if (player1RoundsWon >= 2 || player2RoundsWon >= 2)
        {
            gameEnded = true;
            gameStart = false;
            panelGameSetFinal.SetActive(true);
            panelGameSetPerRound.SetActive(false);

            if (player1RoundsWon >= 2)
            {
                player01Health.PlayWinPose();
                player02Health.PlayLosePose();
                gameSetFinal.text = "Player 1 Win!";
            }
            else
            {
                player01Health.PlayLosePose();
                player02Health.PlayWinPose();
                gameSetFinal.text = "Player 2 Win!";
            }

            return true;
        }

        return false;
    }

    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(2);
        roundNumber++;
        ResetPlayersHealth();
        ResetPlayerPosition();
        ResetTime();
        panelGameSetPerRound.SetActive(false);
        gameEnded = false;
        gameStart = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StartGame());
    }

    private void ResetPlayersHealth()
    {
        player01Health.ResetHealth();
        player02Health.ResetHealth();
    }

    private void ResetPlayerPosition()
    {
        player01Pos.position = player01SpawnPoint;
        player02Pos.position = player02SpawnPoint;
    }

    private void ResetTime()
    {
        time.ResetTime();
    }

    private IEnumerator BeforeStartGame()
    {
        extraCamera.depth = 1;
        mainCamera.depth = 0;
        yield return new WaitForSeconds(0.5f);
        canMoveExtraCam = false;
        ExtraCameraManager.Instance.DisableFollowMainCamera();
        ExtraCameraManager.Instance.MoveCamera(new Vector2 (player01SpawnPoint.x, player01SpawnPoint.y + 1f));
        ExtraCameraManager.Instance.ChangeSizeCamera(2f);
        while (ExtraCameraManager.Instance.IsMoving())
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        player01Health.PlayStartPose();
        while (!canMoveExtraCam)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        canMoveExtraCam = false;
        ExtraCameraManager.Instance.MoveCamera(new Vector2(player02SpawnPoint.x, player02SpawnPoint.y + 1f));
        while (ExtraCameraManager.Instance.IsMoving())
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        player02Health.PlayStartPose();
        while (!canMoveExtraCam)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        ExtraCameraManager.Instance.MoveCamera(mainCamera.transform.position);
        ExtraCameraManager.Instance.ChangeSizeCamera(mainCamera.orthographicSize);
        while (ExtraCameraManager.Instance.IsMoving())
        {
            yield return null;
        }
        extraCamera.depth = 0;
        mainCamera.depth = 1;
        yield return new WaitForSeconds(0.5f);
        ExtraCameraManager.Instance.EnableFollowMainCamera();
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        panelStartGame.SetActive(true);
        gameRound.text = ("Round") + roundNumber;
        startGame.text = null;
        yield return new WaitForSeconds(1f);
        startGame.text = "Start";
        yield return new WaitForSeconds(0.5f);
        panelStartGame.SetActive(false);
        gameStart = true;
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePause)
        {
            IsMainCamera();
            gamePause = true;
            Time.timeScale = 0f;
            if (currentCameraIsMainCam)
            {
                mainPanelPauseGame.SetActive(true);
            }
            else
            {
                extraPanelPauseGame.SetActive(true);
            }
        }
    }

    private void IsMainCamera()
    {
        if (mainCamera.depth > extraCamera.depth)
        {
            currentCameraIsMainCam = true;
        }
        else
        {
            currentCameraIsMainCam = false;
        }
    }
}
