using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerState : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isDefending = false;
    public bool isUsingSkill = false;
    public bool isGettingHurt = false;
    public bool isFacingRight = true;
    public bool isDead = false;
}
