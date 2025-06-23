using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Byakuya_JumpAttack : MonoBehaviour
{
    [SerializeField] private Transform kjPos;
    [SerializeField] private GameObject kjSkillPrefab;
    [SerializeField] private float raycastDistance = 1f;
    private PlayerState playerState;

    private void Start()
    {
        playerState = GetComponentInParent<PlayerState>();
    }

    private void CheckCollision()
    {
        int rayCount = 10;
        float minAngle = -80f;
        float maxAngle = -20f;
        float angleStep = (maxAngle - minAngle) / (rayCount - 1);

        RaycastHit2D? hitOnPlayer = null; 
        RaycastHit2D? lastValidHit = null;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = minAngle + angleStep * i;
            float dir = playerState.isFacingRight ? 1 : -1;
            Vector2 direction = Quaternion.Euler(0, 0, dir * angle) * kjPos.right;

            RaycastHit2D hit = Physics2D.Raycast(kjPos.position, direction, raycastDistance);
            Debug.DrawRay(kjPos.position, direction * raycastDistance, Color.red, 1f);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag(CONSTANT.Player) || hit.collider.CompareTag(CONSTANT.Com))
                {
                    if (hitOnPlayer == null)
                    {
                        hitOnPlayer = hit;
                    }
                }

                lastValidHit = hit;
            }
        }

        if (hitOnPlayer != null)
        {
            GameObject hitSkill = Instantiate(kjSkillPrefab, hitOnPlayer.Value.collider.transform.position, Quaternion.identity); 
            SkillCheckHitUseOverLap skillCheck = hitSkill.GetComponent<SkillCheckHitUseOverLap>();
            if (skillCheck != null)
            {
                skillCheck.SetOwner(this.gameObject);
            }
        }
        else if (lastValidHit != null)
        {
            GameObject skill = Instantiate(kjSkillPrefab, lastValidHit.Value.point, Quaternion.identity); 
            SkillCheckHitUseOverLap skillCheck = skill.GetComponent<SkillCheckHitUseOverLap>();
            if (skillCheck != null)
            {
                skillCheck.SetOwner(this.gameObject);
            }
        }
    }
}

