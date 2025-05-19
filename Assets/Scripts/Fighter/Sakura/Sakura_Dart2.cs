using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Dart2 : Dart
{
    protected override void DartMove()
    {
        Vector2 movement;
        Vector2 direction = this.transform.up;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle - (45f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }
}
