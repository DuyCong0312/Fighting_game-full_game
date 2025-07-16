using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ComDefendState : IPlayerState
{
    private ComStateMachine com;
    private float time = 1f;

    public ComDefendState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        time = 1f;
        com.playerState.isDefending = true;
        com.animator.SetBool(CONSTANT.isDefend, true);
    }

    public void UpdateState()
    {
        time -= Time.deltaTime;
        if (time < 0f || !com.groundCheck.isGround) 
        {
            com.playerState.isDefending = false;
            com.animator.SetBool(CONSTANT.isDefend, false);
            com.ChangeState(new ComIdleState(com));
        }
    }

    public void ExitState()
    {
    }

}


