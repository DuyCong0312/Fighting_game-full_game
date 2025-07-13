using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComIdleState : IPlayerState
{
    private ComStateMachine com;

    public ComIdleState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        com.rb.velocity = new Vector2(0, com.rb.velocity.y);
        com.animator.SetInteger(CONSTANT.CurrentState, 0);
    }

    public void UpdateState()
    {
    }

    public void ExitState()
    {

    }
}