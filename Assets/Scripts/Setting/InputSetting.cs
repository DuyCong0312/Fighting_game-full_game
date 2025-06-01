using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// not completed yet
public class InputSetting : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button applyButton;
    public Button cancelButton;
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
    private PlayerInputSO backupInput;
    private bool waitingForKey = false;

    private enum KeyType
    {
        Up, Down, Left, Right, Attack, Jump, Dash, Skill, SpSkill
    }

    public void SetPlayerInput(PlayerInputSO inputSO)
    {
        playerInput = inputSO; 
        
        backupInput = ScriptableObject.CreateInstance<PlayerInputSO>();
        CopyInput(playerInput, backupInput);

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
        if (backupInput != null && playerInput != null)
        {
            CopyInput(playerInput, backupInput);
        }
    }

    private void CancelChanges()
    {
        if (backupInput != null && playerInput != null)
        {
            CopyInput(backupInput, playerInput);
            RefreshKeyTexts();
        }
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
}
