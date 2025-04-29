using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetState : MonoBehaviour
{
    private PlayerState playerState;

    private void Start()
    {
        playerState = GetComponentInParent<PlayerState>();
    }
    private void StopSkill()
    {
        playerState.isUsingSkill = false;
    }

    private void CanGetHurt()
    {
        playerState.isGettingHurt = false;
    }
}
