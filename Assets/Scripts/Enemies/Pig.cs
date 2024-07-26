using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy
{
    [SerializeField] private float aggressiveTime;
    private float aggressiveTimeCounter;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        aggressiveTimeCounter -= Time.deltaTime;

        
        if (aggressiveTimeCounter < 0)
        {
            WalkAround();
        }
        else
        {
            AggressiveWalk();
        }

        CollisionChecks();
        AnimationController();
    }

    private void AggressiveWalk()
    {
        Move(2 * moveSpeed * facingDirection, rb.velocity.y);

        if (isWallDetected || !isGrounded)
            Flip();
    }

   

    public override void Damage()
    {
        if(aggressiveTimeCounter < 0) // if enemy is in non agressive mode and got damage, then make it aggressive and killable after any damage given by player
        {
            aggressiveTimeCounter = aggressiveTime;
            anim.SetTrigger("makeAngry");
        }
        else if(aggressiveTimeCounter < aggressiveTime - 0.1f) // if enemy has taken damage and got taken damage again, then kill it.
        {
            base.Damage();
        }
        
    }
}
