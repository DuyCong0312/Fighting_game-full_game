using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpecialEffect : MonoBehaviour
{
    [SerializeField] private Sprite iSkillAttack;
    [SerializeField] private Sprite wiSkillAttack;
    [SerializeField] private Sprite siSkillAttack;
    public bool callWIEffect = false;
    public bool callSIEffect = false;
    private Animator anim;
    private PlayerState playerState;
    public enum SpecialEffectType { IskillAttack, WIskillAttack, SIskillAttack }

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerState = GetComponentInParent<PlayerState>();
    }

    public void SpecialEffectSpawn(SpecialEffectType type)
    {
        Time.timeScale = 0f;
        playerState.allowCheck = false;
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        switch (type)
        {
            case SpecialEffectType.IskillAttack:
                CallIskillAttackEffect();
                break;
            case SpecialEffectType.WIskillAttack:
                CallWIskillAttackEffect();
                break;
            case SpecialEffectType.SIskillAttack:
                CallSIskillAttackEffect();
                break;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.gamePause)
        {
            anim.speed = 0f;
        }
        else
        {
            anim.speed = 1f;
        }
    }

    private void EndStartPose()
    {
        GameManager.Instance.canMoveExtraCam = true;
    }

    private void CallIskillAttackEffect()
    {
        ExtraCameraManager.Instance.CallSpecialAttackEffect(this.transform.rotation, iSkillAttack);
    }
    private void CallWIskillAttackEffect()
    {
        ExtraCameraManager.Instance.CallSpecialAttackEffect(this.transform.rotation, wiSkillAttack);
        callWIEffect = false;
    }
    private void CallSIskillAttackEffect()
    {
        ExtraCameraManager.Instance.CallSpecialAttackEffect(this.transform.rotation, siSkillAttack);
        callSIEffect = false;
    }

    private void StopSkillAttackEffect()
    {
        anim.updateMode = AnimatorUpdateMode.Normal;
        Time.timeScale = 1f;
        playerState.allowCheck = true;
        ExtraCameraManager.Instance.StopSpecialAttackEffect();
    }
}
