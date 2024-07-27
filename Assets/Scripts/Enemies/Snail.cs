using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        WalkAround();
        CollisionChecks();
        AnimationController();
    }
}
