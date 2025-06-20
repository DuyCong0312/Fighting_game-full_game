using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Renji_Iskill : MonoBehaviour
{
    private KnockBack knockBack;
    [SerializeField] private GameObject IskillObject;
    [SerializeField] private float speed;

    private void Start()
    {
        knockBack = GetComponentInParent<KnockBack>();
    }

    private void ActiveRenjiIskill()
    {
        Transform opponent = knockBack.opponentDirection;
        float direction = opponent.eulerAngles.y == 0f ? 1f : -1f;

        for (int i = 0; i < 3; i++)
        {
            Vector2 pos1 = new Vector2(opponent.position.x - 2f * direction, (opponent.position.y - 2f) + 2f * i);
            Vector2 pos2 = new Vector2(opponent.position.x + 2f * direction, (opponent.position.y - 2f) + 2f * i);

            Quaternion rot1 = opponent.rotation * Quaternion.Euler(0, 0, 45 - i * 45);
            Quaternion rot2 = opponent.rotation * Quaternion.Euler(0, 180, 45 - i * 45);

            var obj1 = Instantiate(IskillObject, pos1, rot1).GetComponent<Renji_dart>();
            var obj2 = Instantiate(IskillObject, pos2, rot2).GetComponent<Renji_dart>();

            obj1.target = opponent;
            obj2.target = opponent;
        }
    }
}
