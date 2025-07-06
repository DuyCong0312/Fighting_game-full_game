using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceContinueAnimation : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;
    private KnockBack knockBack;
    private PlayerStateMachine player;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;
    [SerializeField] private string nameAnimatorClip;
    [SerializeField] private string idleClip;
    [SerializeField] private float distanceToOpponent = 2f;
    [SerializeField] private float distanceFromStart = 5f;
    [SerializeField] private bool acceptChange = false;
    private Vector3 initialPosition;
    private Transform opponentTransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = animator.transform.parent;
        knockBack = animator.GetComponentInParent<KnockBack>();
        rb = animator.GetComponentInParent<Rigidbody2D>();
        player = animator.GetComponentInParent<PlayerStateMachine>();
        playerState = animator.GetComponentInParent<PlayerState>();
        effectAfterImage = animator.GetComponentInParent<SpawnEffectAfterImage>();
        initialPosition = playerTransform.position;
        opponentTransform = knockBack.opponentDirection;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToOpponentValue = Vector2.Distance(playerTransform.position, opponentTransform.position);
        float distanceFromStartValue = Vector2.Distance(playerTransform.position, initialPosition);

        if (distanceToOpponentValue <= distanceToOpponent || distanceFromStartValue >= distanceFromStart)
        {
            rb.velocity = Vector2.zero;
        }

        if (distanceToOpponentValue <= distanceToOpponent)
        {
            animator.Play(nameAnimatorClip);
        }
        else if (distanceFromStartValue >= distanceFromStart)
        {
            if (acceptChange)
            {
                animator.Play(nameAnimatorClip);
            }
            else
            {
                playerState.isUsingSkill = false;
                rb.gravityScale = player.originalGravity;
                effectAfterImage.StopAfterImageEffect();
                animator.Play(idleClip);
            }
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
