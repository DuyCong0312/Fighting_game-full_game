using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DashState : IPlayerState
{
    private PlayerStateMachine player;
    private float dashTimer;
    private float originalGravity;

    public DashState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        dashTimer = player.dashTime;

        SetLayerRecursively(player.gameObject, player.dashLayer);
        player.animator.SetBool(CONSTANT.isDashing, true);

        originalGravity = player.rb.gravityScale;
        player.rb.gravityScale = 0;

        float direction = player.transform.eulerAngles.y == 0 ? 1 : -1;
        player.rb.velocity = new Vector2(direction * player.dashPower, 0);

        player.effectAfterImage.StartAfterImageEffect();
        player.StartDashCooldown();
    }

    public void UpdateState()
    {
        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0)
        {
            player.rb.gravityScale = originalGravity;
            player.animator.SetBool(CONSTANT.isDashing, false);
            player.effectAfterImage.StopAfterImageEffect();
            SetLayerRecursively(player.gameObject, player.defaultLayer);
            BackToState();
        }
    }

    private void BackToState()
    {
        if (player.groundCheck.isGround)
        {
            player.ChangeState(new IdleState(player));
            return;
        }
        else
        {
            player.ChangeState(new FallingState(player));
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
        player.rb.gravityScale = originalGravity;
        player.animator.SetBool(CONSTANT.isDashing, false);
        player.effectAfterImage.StopAfterImageEffect();

        SetLayerRecursively(player.gameObject, player.defaultLayer);
    }
}

