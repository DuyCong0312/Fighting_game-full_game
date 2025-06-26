using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi_KU : Projectile
{
    protected override void ProjectileMove()
    {
        Vector2 movement;
        float yrotation = this.transform.rotation.eulerAngles.y;
        int directionAngle = yrotation == 0f ? 1 : -1;
        Vector2 direction = this.transform.right;
        float currentAngle = Mathf.Atan2(direction.y, direction.x);
        float angle = currentAngle + directionAngle * (-45f * Mathf.Rad2Deg);
        movement.x = Mathf.Cos(angle);
        movement.y = Mathf.Sin(angle);

        rb.velocity = movement.normalized * speed;
    }


    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation);
    }
}
