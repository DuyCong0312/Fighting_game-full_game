using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private PlayerInputSO playerInput;

    private Animator anim;
    private CheckGround groundCheck;
    private PlayerState playerState;
    private PlayerRage playerRage;

    private void Start()
    {
        StartCoroutine(InitializeComponentsInChildren());
        groundCheck = GetComponent<CheckGround>();
        playerState = GetComponent<PlayerState>();
        playerRage = GetComponent<PlayerRage>();
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

        if (!GameManager.Instance.gameStart
            || GameManager.Instance.gameEnded
            || playerState.isUsingSkill
            || playerState.isDefending
            || playerState.isAttacking
            || playerState.isGettingHurt)
        {
            return;
        }
        ActiveRangedAttack();
        ActiveSpecialAttack();
    }

    private void ActiveRangedAttack()
    {
        if (Input.GetKeyDown(playerInput.rangedAttack))
        {
            playerRage.GetRage(5f);
            PerformSkill(CONSTANT.Uskill, CONSTANT.UKskill);
        }
    }

    private void ActiveSpecialAttack()
    {
        if (Input.GetKeyDown(playerInput.specialAttack))
        {
            if (playerRage.currentRage >= 30f)
            {
                playerRage.CostRage(30f);
                PerformSkill(CONSTANT.Iskill, CONSTANT.IKskill);
            }
            else
            {
                return;
            }

        }
    }

    private void PerformSkill(string GroundAnimationTrigger, string AirAnimationName)
    {
        playerState.isDefending = false;
        playerState.isUsingSkill = true;
        if (groundCheck.isGround)
        {
            anim.SetTrigger(GroundAnimationTrigger);
        }
        else
        {
            bool hasAnimation = false;
            foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
            {
                if (clip.name == AirAnimationName)
                {
                    hasAnimation = true;
                    break;
                }
            }
            if (!hasAnimation)
            {
                playerState.isUsingSkill = false;
                return;
            }
            anim.Play(AirAnimationName);
        }
    }
}
