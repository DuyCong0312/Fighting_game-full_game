using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGroundContinueAnimation : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;
    private CheckGround groundCheck;
    private PlayerStateMachine player;
    private ComStateMachine com;
    private SpawnEffectAfterImage effectAfterImage;
    [SerializeField] protected string nameAnimatorClip;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = animator.transform;
        groundCheck = animator.GetComponentInParent<CheckGround>();
        rb = animator .GetComponentInParent<Rigidbody2D>();
        effectAfterImage = animator.GetComponentInParent<SpawnEffectAfterImage>();
        player = animator.GetComponentInParent<PlayerStateMachine>();
        if (player == null)
        {
            com = animator.GetComponentInParent<ComStateMachine>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (groundCheck.isGround)
        {
            animator.Play(nameAnimatorClip);
            rb.velocity = Vector2.zero;
            if (player != null)
            {
                rb.gravityScale = player.originalGravity;
            }
            else if (com != null)
            {
                rb.gravityScale = com.originalGravity;
            }
            effectAfterImage.StopAfterImageEffect();
            Vector3 currentRotation = playerTransform.rotation.eulerAngles;
            playerTransform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion  
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
