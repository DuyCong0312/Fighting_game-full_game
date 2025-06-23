using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffectWhenTouchGround : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius;
    private bool isGround;

    private void Update()
    {
        GroundCheck();
    }
    private void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer);
        if (isGround)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, circleRadius);
    }

}
