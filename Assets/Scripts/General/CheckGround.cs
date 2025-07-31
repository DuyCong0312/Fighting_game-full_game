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
    public bool isFloor;
    public bool isJumping = false;

    [Header("Materials")]
    [SerializeField] private PhysicsMaterial2D groundMaterial;
    [SerializeField] private PhysicsMaterial2D airMaterial;

    private Collider2D coll;
    private GameObject floor;
    private bool moveDown = false;

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
        if (!moveDown)
        {
            isGround = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer);
        }

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

    public void MoveDownThroughFloor()
    {
        StartCoroutine(DisableCollision());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            floor = collision.gameObject;
            isFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            floor = null;
            isFloor = false;
        }
    }

    private IEnumerator DisableCollision()
    {
        Collider2D floorColl = floor.GetComponent<Collider2D>();

        isGround = false;
        moveDown = true;
        Physics2D.IgnoreCollision(coll, floorColl, true);
        yield return new WaitForSeconds(0.5f);
        moveDown = false;
        Physics2D.IgnoreCollision(coll, floorColl, false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, circleRadius);
    }

}
