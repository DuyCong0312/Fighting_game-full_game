using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
public class JumpingState : IPlayerState
{
    private PlayerStateMachine player;

    public JumpingState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
        player.animator.SetInteger(CONSTANT.CurrentState, 2);
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.jump, player.jumpPos, player.transform.rotation);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
    }

    public void UpdateState()
    {
        float movement = 0f;

        if (Input.GetKey(player.playerInput.moveLeft))
        {
            if (player.playerState.isFacingRight)
            {
                player.transform.eulerAngles = new Vector3(0, 180, 0);
                player.playerState.isFacingRight = false;
            }
            movement = -1f;
        }
        else if (Input.GetKey(player.playerInput.moveRight))
        {
            if (!player.playerState.isFacingRight)
            {
                player.transform.eulerAngles = new Vector3(0, 0, 0);
                player.playerState.isFacingRight = true;
            }
            movement = 1f;
        }
        player.rb.velocity = new Vector2(player.speed * movement, player.rb.velocity.y);

        if (player.rb.velocity.y < 0)
        {
            player.ChangeState(new FallingState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.dash) && player.CanDash())
        {
            player.ChangeState(new DashState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.attack))
        {
            player.ChangeState(new AttackState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.rangedAttack) && !player.playerState.isUsingSkill)
        {
            player.rb.gravityScale = 0f;
            player.rb.velocity = Vector2.zero;
            player.ChangeState(new RangedAttackState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.specialAttack) && !player.playerState.isUsingSkill)
        {
            player.rb.gravityScale = 0f;
            player.rb.velocity = Vector2.zero;
            player.ChangeState(new SpecialAttackState(player));
            return;
        }
    }

    public void ExitState()
    {
    }
}
