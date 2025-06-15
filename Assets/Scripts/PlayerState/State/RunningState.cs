using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RunningState : IPlayerState
{
    private PlayerStateMachine player;

    public RunningState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.animator.SetInteger(CONSTANT.CurrentState, 1);
    }

    public void UpdateState()
    {

        if (!Input.GetKey(player.playerInput.moveLeft) && !Input.GetKey(player.playerInput.moveRight))
        {
            player.ChangeState(new IdleState(player));
            return;
        }

        if (player.playerState.isAttacking || player.playerState.isGettingHurt)
        {
            player.rb.velocity = new Vector2(0, player.rb.velocity.y);
            return;
        }

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

        if (Input.GetKeyDown(player.playerInput.jump) && player.groundCheck.isGround)
        {
            player.ChangeState(new JumpingState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.attack))
        {
            player.ChangeState(new AttackState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.dash) && player.CanDash())
        {
            player.ChangeState(new DashState(player));
            return;
        }

        if (Input.GetKey(player.playerInput.defense) && player.groundCheck.isGround)
        {
            player.ChangeState(new DefendState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.rangedAttack) && !player.playerState.isUsingSkill)
        {
            player.ChangeState(new RangedAttackState(player));
            return;
        }

        if (Input.GetKeyDown(player.playerInput.specialAttack) && !player.playerState.isUsingSkill)
        {
            player.ChangeState(new SpecialAttackState(player));
            return;
        }

    }

    public void ExitState()
    {
    }
}
