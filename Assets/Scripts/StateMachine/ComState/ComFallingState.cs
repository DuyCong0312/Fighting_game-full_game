using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComFallingState : IPlayerState
{
    private ComStateMachine com;

    public ComFallingState(ComStateMachine com)
    {
        this.com = com;
    }

    public void EnterState()
    {
        com.animator.SetInteger(CONSTANT.CurrentState, 3);
    }

    public void UpdateState()
    {
    }

    public void ExitState()
    {
    }

}
