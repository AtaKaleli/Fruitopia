using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : Enemy
{

    private bool isUpperGrounded;
    private int flyingDirection = 1; // 1 represents up, -1 represents down
    


    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        CollisionChecks();
        FlyAround();
    }

    private  void FlyAround()
    {
        Move(moveSpeed * facingDirection, moveSpeed * flyingDirection);
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isUpperGrounded = Physics2D.Raycast(groundCheck.position, Vector2.up, groundCheckDistance, whatIsGround);
        FlipController();
    }

    private void FlipController()
    {

        if (isWallDetected)
        {
            Flip();
        }

        if (isGrounded)
        {
            flyingDirection = 1;
        }
        else if (isUpperGrounded)
        {
            flyingDirection = -1;
        }
        
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y + groundCheckDistance));
    }

  
}
