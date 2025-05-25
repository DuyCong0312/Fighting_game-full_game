using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Dart2 : Projectile
{
    protected override void ProjectileMove()
    {
        Vector2 movement;
        float yRotation = transform.rotation.eulerAngles.y;
        int directionAngle = yRotation == 0f ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (45f * Mathf.Deg2Rad);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }
}
