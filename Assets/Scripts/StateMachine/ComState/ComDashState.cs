using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComDashState : IPlayerState
{
    private ComStateMachine com;
    private float dashTimer;

    public ComDashState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        dashTimer = com.dashTime;

        SetLayerRecursively(com.gameObject, com.dashLayer);
        com.animator.SetBool(CONSTANT.isDashing, true);

        com.rb.gravityScale = 0;

        float direction = com.transform.eulerAngles.y == 0 ? 1 : -1;
        com.rb.velocity = new Vector2(direction * com.dashPower, 0);

        PlaySoundAndEffect();
        com.effectAfterImage.StartAfterImageEffect();
        com.StartDashCooldown();
    }

    private void PlaySoundAndEffect()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.dash);
        if (com.groundCheck.isGround)
        {
            EffectManager.Instance.SpawnEffect(EffectManager.Instance.groundDash, com.dashPos, Quaternion.Euler(0, 180, 0) * com.transform.rotation);
        }
        else
        {
            EffectManager.Instance.SpawnEffect(EffectManager.Instance.airDash, com.dashPos, Quaternion.Euler(0, 180, 0) * com.transform.rotation);
        }
    }
    public void UpdateState()
    {
        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0)
        {
            com.rb.velocity = Vector2.zero;
            com.rb.gravityScale = com.originalGravity;
            com.animator.SetBool(CONSTANT.isDashing, false);
            com.effectAfterImage.StopAfterImageEffect();
            SetLayerRecursively(com.gameObject, com.defaultLayer);
            BackToState();
        }
    }

    private void BackToState()
    {
        if (com.groundCheck.isGround)
        {
            com.ChangeState(new ComIdleState(com));
            return;
        }
        else
        {
            com.ChangeState(new ComFallingState(com));
            return;
        }
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public void ExitState()
    {
        com.rb.gravityScale = com.originalGravity;
        com.animator.SetBool(CONSTANT.isDashing, false);
        com.effectAfterImage.StopAfterImageEffect();

        SetLayerRecursively(com.gameObject, com.defaultLayer);
    }

}
