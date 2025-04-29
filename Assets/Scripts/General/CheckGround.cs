using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{

    [Header("Ground Check Setting")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius;
    public bool isGround;
    public bool isJumping = false;


    [Header("Materials")]
    [SerializeField] private PhysicsMaterial2D groundMaterial;
    [SerializeField] private PhysicsMaterial2D airMaterial;
    private Collider2D coll;
    
    private void Start()
    {
        coll = GetComponentInChildren<Collider2D>();
    }

    private void Update()
    {
        GroundCheck();
    }
    private void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer);

        isJumping = isGround ? false : true;
        if (isGround)
        {
            coll.sharedMaterial = groundMaterial;
        }
        else
        {
            coll.sharedMaterial = airMaterial;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, circleRadius);
    }

}
