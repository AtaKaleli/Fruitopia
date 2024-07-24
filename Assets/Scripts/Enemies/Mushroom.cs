using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        
        
        
        WalkAround();
        CollisionChecks();
        AnimationController();
    }

    private void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
    }
}
