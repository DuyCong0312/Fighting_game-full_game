using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialMove : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private PlayerInputSO playerInput;

    [Header("Special Move")]
    [SerializeField] private SpecialMoveSO defenseAttack;
    [SerializeField] private SpecialMoveSO defenseRangedAttack;
    [SerializeField] private SpecialMoveSO defenseSpecialAttack;
    [SerializeField] private SpecialMoveSO upAttack;
    [SerializeField] private SpecialMoveSO upRangedAttack;
    [SerializeField] private SpecialMoveSO upSpecialAttack;

    private Animator anim;
    private PlayerState playerState;
    private PlayerRage playerRage;
    private CheckGround groundCheck;

    private void Start()
    {
        StartCoroutine(InitializeComponentsInChildren());
        playerState = GetComponent<PlayerState>();
        playerRage = GetComponent<PlayerRage>();
        groundCheck = GetComponent<CheckGround>();
    }

    private IEnumerator InitializeComponentsInChildren()
    {
        while (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
            yield return null;
        }
    }

    private void Update()
    {
        UpInputActive();
        HandleSpecialMoveInput();
    }

    private void UpInputActive()
    {
        if (Input.GetKey(playerInput.specialMoveUpInput) && !playerState.isUpInputPress && groundCheck.isGround)
        {
            playerState.isUpInputPress = true;
        }
        else if (Input.GetKeyUp(playerInput.specialMoveUpInput))
        {
            playerState.isUpInputPress = false;
        }
    }

    private void HandleSpecialMoveInput()
    {
        if (playerState.isDefending)
        {
            PerformSpecialMove(defenseAttack, defenseRangedAttack, defenseSpecialAttack);
        }

        else if (playerState.isUpInputPress)
        {
            PerformSpecialMove(upAttack, upRangedAttack, upSpecialAttack);
        }
    }
    private void PerformSpecialMove(SpecialMoveSO attack, SpecialMoveSO rangedAttack, SpecialMoveSO specialAttack)
    {
        if (Input.GetKeyDown(playerInput.attack))
        {
            PlaySpecialMove(attack);
        }
        else if (Input.GetKeyDown(playerInput.rangedAttack))
        {
            PlaySpecialMove(rangedAttack);
        }
        else if (Input.GetKeyDown(playerInput.specialAttack))
        {
            PlaySpecialMove(specialAttack);
        }
    }

    private void PlaySpecialMove(SpecialMoveSO move)
    {
        if (move == null || playerRage.currentRage < move.rageCost) return;
        playerState.isUsingSkill = true;
        playerState.isDefending = false;
        playerState.isAttacking =false;
        anim.Play(move.animationName);
        playerRage.CostRage(move.rageCost);
    }
}
