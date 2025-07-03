using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Naruto_Uskill : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("U Skill")]
    [SerializeField] private GameObject SkillPrefab;
    [SerializeField] private Transform skillPos;

    [Header("U+K Skill")]
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerState = GetComponentInParent<PlayerState>();
        effectAfterImage = GetComponentInParent<SpawnEffectAfterImage>();
    }
    private void ActiveNarutoUskill()
    {
        NarutoUSkill(1, 0.5f);
    }

    private void NarutoUSkill(int extraInstances, float offset)
    {
        Instantiate(SkillPrefab, skillPos.position, transform.rotation);

        for (int i = 1; i <= extraInstances; i++)
        {
            Instantiate(SkillPrefab, new Vector2(skillPos.position.x, skillPos.position.y + offset), transform.rotation);
            Instantiate(SkillPrefab, new Vector2(skillPos.position.x, skillPos.position.y - offset), transform.rotation);
        }
    }

    private void ActiveNarutoUKskill()
    {
        effectAfterImage.StartAfterImageEffect();
        CalculateVelocityKJ();
    }

    private void CalculateVelocityKJ()
    {
        Vector2 movement;
        int directionAngle = playerState.isFacingRight ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (-45f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }
}
