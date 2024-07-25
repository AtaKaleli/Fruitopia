using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        idleTimeCounter -= Time.deltaTime;

        WalkAround();
        CollisionChecks();
        AnimationController();

    }

    private void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
    }
}
