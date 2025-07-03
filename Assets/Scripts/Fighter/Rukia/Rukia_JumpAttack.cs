using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rukia_JumpAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;

    [SerializeField] private float speed;
    [SerializeField] private Transform effectTransform;
    [SerializeField] private GameObject effect;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
    }

    private void ActiveRukiaJumpAttack()
    {
        effectAfterImage.StartAfterImageEffect();
        CalculateVelocityKJ();
        Instantiate(effect, effectTransform.position, effectTransform.rotation, effectTransform);
    }

    private void CalculateVelocityKJ()
    {
        Vector2 movement;
        int directionAngle = playerState.isFacingRight ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (-70f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }
}
