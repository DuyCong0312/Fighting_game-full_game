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
    public bool isUpInputPress = false;
    public bool immuneToDamage = false;
    public bool allowCheck = true;
    public bool hitBorderMap = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.MapBorder))
        {
            hitBorderMap = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.MapBorder))
        {
            hitBorderMap = false;
        }
    }
}
