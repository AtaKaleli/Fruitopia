using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBird : Enemy
{
    [SerializeField] private float jumpForce;

    protected override void Start()
    {
        base.Start();
        idleTimeCounter = idleTime;
    }


    
    void Update()
    {
        if (isGrounded)
            idleTimeCounter -= Time.deltaTime;


        CollisionChecks();
        JumpFallController();
        FatBirdAnimationController();
    }

    private void FatBirdAnimationController()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void JumpFallController()
    {
        if(idleTimeCounter < 0)
        {
            Move(0, jumpForce);
            idleTimeCounter = idleTime;
        }
    }

   

}

