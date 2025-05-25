using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_darts : Projectile
{
    protected override void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation);
    }
}
