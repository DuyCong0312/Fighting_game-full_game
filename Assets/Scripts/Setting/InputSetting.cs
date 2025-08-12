using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// not completed yet
public class InputSetting : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] private PlayerInputSO player1Input;
    [SerializeField] private PlayerInputSO player2Input;

    [Header("UI Buttons")]
    public Button applyButton;
    public Button cancelButton;
    [SerializeField] private Button Player01SettingButton;
    [SerializeField] private Button Player02SettingButton;
    [SerializeField] private Button UpButton;
    [SerializeField] private Button DownButton;
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Button AttackButton;
    [SerializeField] private Button JumpButton;
    [SerializeField] private Button DashButton;
    [SerializeField] private Button SkillButton;
    [SerializeField] private Button SpSkillButton;

    [Header("UI Texts")]
    [SerializeField] private TMP_Text UpButtonText;
    [SerializeField] private TMP_Text DownButtonText;
    [SerializeField] private TMP_Text LeftButtonText;
    [SerializeField] private TMP_Text RightButtonText;
    [SerializeField] private TMP_Text AttackButtonText;
    [SerializeField] private TMP_Text JumpButtonText;
    [SerializeField] private TMP_Text DashButtonText;
    [SerializeField] private TMP_Text SkillButtonText;
    [SerializeField] private TMP_Text SpSkillButtonText;

    private PlayerInputSO playerInput;
    private PlayerInputSO backupInputP1;
    private PlayerInputSO backupInputP2;
    private int currentPlayerIndex = 0;
    private bool waitingForKey = false;

    private enum KeyType
    {
        Up, Down, Left, Right, Attack, Jump, Dash, Skill, SpSkill
    }

    private void SetPlayerInput(PlayerInputSO inputSO, int playerIndex)
    {
        playerInput = inputSO;
        currentPlayerIndex = playerIndex;

        LoadPlayerInput(playerInput, currentPlayerIndex);
        if (currentPlayerIndex == 0)
        {
            if (backupInputP1 == null) backupInputP1 = ScriptableObject.CreateInstance<PlayerInputSO>();
            player1Input = playerInput;
            CopyInput(player1Input, backupInputP1);
        }
        else
        {
            if (backupInputP2 == null) backupInputP2 = ScriptableObject.CreateInstance<PlayerInputSO>();
            player2Input = playerInput;
            CopyInput(player2Input, backupInputP2);
        }

        RefreshKeyTexts();
    }

    private void Start()
    {
        SetupButtonListeners();
        applyButton.onClick.AddListener(ApplyChanges);
        cancelButton.onClick.AddListener(CancelChanges);
    }

    private void SetupButtonListeners()
    {
        Player01SettingButton.onClick.AddListener(() => SetPlayerInput(player1Input, 0));
        Player02SettingButton.onClick.AddListener(() => SetPlayerInput(player2Input, 1));
        UpButton.onClick.AddListener(() => StartRebinding(KeyType.Up, UpButtonText));
        DownButton.onClick.AddListener(() => StartRebinding(KeyType.Down, DownButtonText));
        LeftButton.onClick.AddListener(() => StartRebinding(KeyType.Left, LeftButtonText));
        RightButton.onClick.AddListener(() => StartRebinding(KeyType.Right, RightButtonText));
        AttackButton.onClick.AddListener(() => StartRebinding(KeyType.Attack, AttackButtonText));
        JumpButton.onClick.AddListener(() => StartRebinding(KeyType.Jump, JumpButtonText));
        DashButton.onClick.AddListener(() => StartRebinding(KeyType.Dash, DashButtonText));
        SkillButton.onClick.AddListener(() => StartRebinding(KeyType.Skill, SkillButtonText));
        SpSkillButton.onClick.AddListener(() => StartRebinding(KeyType.SpSkill, SpSkillButtonText));
    }

    private void RefreshKeyTexts()
    {
        if (playerInput == null) return;

        UpButtonText.text = ":" + playerInput.specialMoveUpInput.ToString();
        DownButtonText.text = ":" + playerInput.defense.ToString();
        LeftButtonText.text = ":" + playerInput.moveLeft.ToString();
        RightButtonText.text = ":" + playerInput.moveRight.ToString();
        AttackButtonText.text = ":" + playerInput.attack.ToString();
        JumpButtonText.text = ":" + playerInput.jump.ToString();
        DashButtonText.text = ":" + playerInput.dash.ToString();
        SkillButtonText.text = ":" + playerInput.rangedAttack.ToString();
        SpSkillButtonText.text = ":" + playerInput.specialAttack.ToString();
    }

    private void StartRebinding(KeyType keyType, TMP_Text buttonText)
    {
        if (waitingForKey || playerInput == null) return;
        StartCoroutine(WaitForKey(keyType, buttonText));
    }

    private IEnumerator WaitForKey(KeyType keyType, TMP_Text buttonText)
    {
        waitingForKey = true;
        buttonText.text = "Press a key...";

        yield return null;

        bool keySet = false;
        while (!keySet)
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    SetKey(keyType, kcode);
                    buttonText.text = ":" + kcode.ToString();
                    keySet = true;
                    break;
                }
            }
            yield return null;
        }

        waitingForKey = false;
    }

    private void SetKey(KeyType keyType, KeyCode newKey)
    {
        switch (keyType)
        {
            case KeyType.Up: playerInput.specialMoveUpInput = newKey; break;
            case KeyType.Down: playerInput.defense = newKey; break;
            case KeyType.Left: playerInput.moveLeft = newKey; break;
            case KeyType.Right: playerInput.moveRight = newKey; break;
            case KeyType.Attack: playerInput.attack = newKey; break;
            case KeyType.Jump: playerInput.jump = newKey; break;
            case KeyType.Dash: playerInput.dash = newKey; break;
            case KeyType.Skill: playerInput.rangedAttack = newKey; break;
            case KeyType.SpSkill: playerInput.specialAttack = newKey; break;
        }
    }

    private void ApplyChanges()
    {
        if (player1Input != null && backupInputP1 != null)
            CopyInput(player1Input, backupInputP1);

        if (player2Input != null && backupInputP2 != null)
            CopyInput(player2Input, backupInputP2);

        if (player1Input != null) SavePlayerInput(player1Input, 0);
        if (player2Input != null) SavePlayerInput(player2Input, 1);
    }

    private void CancelChanges()
    {
        if (player1Input != null && backupInputP1 != null)
            CopyInput(backupInputP1, player1Input);

        if (player2Input != null && backupInputP2 != null)
            CopyInput(backupInputP2, player2Input);

        RefreshKeyTexts();
    }

    private void CopyInput(PlayerInputSO from, PlayerInputSO to)
    {
        to.specialMoveUpInput = from.specialMoveUpInput;
        to.defense = from.defense;
        to.moveLeft = from.moveLeft;
        to.moveRight = from.moveRight;
        to.attack = from.attack;
        to.jump = from.jump;
        to.dash = from.dash;
        to.rangedAttack = from.rangedAttack;
        to.specialAttack = from.specialAttack;
    }

    private void SavePlayerInput(PlayerInputSO player, int playerNumber)
    {
        if (playerNumber == 0)
        {
            PlayerPrefs.SetInt(CONSTANT.P1_Attack, (int)player.attack);
            PlayerPrefs.SetInt(CONSTANT.P1_Dash, (int)player.dash);
            PlayerPrefs.SetInt(CONSTANT.P1_Down, (int)player.defense);
            PlayerPrefs.SetInt(CONSTANT.P1_Jump, (int)player.jump);
            PlayerPrefs.SetInt(CONSTANT.P1_Left, (int)player.moveLeft);
            PlayerPrefs.SetInt(CONSTANT.P1_Right, (int)player.moveRight);
            PlayerPrefs.SetInt(CONSTANT.P1_Skill, (int)player.rangedAttack);
            PlayerPrefs.SetInt(CONSTANT.P1_SpSkill, (int)player.specialAttack);
            PlayerPrefs.SetInt(CONSTANT.P1_Up, (int)player.specialMoveUpInput);
        }
        else
        {
            PlayerPrefs.SetInt(CONSTANT.P2_Attack, (int)player.attack);
            PlayerPrefs.SetInt(CONSTANT.P2_Dash, (int)player.dash);
            PlayerPrefs.SetInt(CONSTANT.P2_Down, (int)player.defense);
            PlayerPrefs.SetInt(CONSTANT.P2_Jump, (int)player.jump);
            PlayerPrefs.SetInt(CONSTANT.P2_Left, (int)player.moveLeft);
            PlayerPrefs.SetInt(CONSTANT.P2_Right, (int)player.moveRight);
            PlayerPrefs.SetInt(CONSTANT.P2_Skill, (int)player.rangedAttack);
            PlayerPrefs.SetInt(CONSTANT.P2_SpSkill, (int)player.specialAttack);
            PlayerPrefs.SetInt(CONSTANT.P2_Up, (int)player.specialMoveUpInput);
        }
        PlayerPrefs.Save();
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
