using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueAnimationHeavyHurt : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private CheckGround groundCheck;
    private Vector2 jumpPos;
    [SerializeField] private string nameAnimatorClip;
    [SerializeField] private GameObject heavyHurtEffect01;
    [SerializeField] private GameObject heavyHurtEffect02;
    [SerializeField] private float blowUpPower;

    private bool blowUpCalled = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!blowUpCalled)
        {
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            groundCheck = animator.GetComponentInParent<CheckGround>();
            rb = animator.GetComponentInParent<Rigidbody2D>();
            blowUpCalled = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (groundCheck.isGround)
        {
            Vector2 jumpPosValue = new Vector2(spriteRenderer.bounds.center.x, spriteRenderer.bounds.min.y);
            jumpPos = jumpPosValue;
            animator.Play(nameAnimatorClip);
            Instantiate(heavyHurtEffect02, jumpPos, Quaternion.identity);
            Instantiate(heavyHurtEffect01, jumpPos, Quaternion.identity);
            rb.velocity = Vector2.zero;
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
