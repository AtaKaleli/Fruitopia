using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : Enemy
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
        DuckAnimationController();
    }

    private void DuckAnimationController()
    {
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void JumpFallController()
    {
        if(idleTimeCounter < 0)
        {
            idleTimeCounter = idleTime;
            anim.SetTrigger("readyForJump");
        }
    }

    public void MakeDuckJump()
    {
        Move(0, jumpForce);
    }
}
