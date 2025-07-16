using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComHurtState : IPlayerState
{
    private ComStateMachine com;

    public ComHurtState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        com.animator.SetTrigger(CONSTANT.getHurt);
        com.Flipped();
    }

    public void UpdateState()
    {
        if (!com.playerState.isGettingHurt)
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
    }

    public void ExitState()
    {
    }
}

