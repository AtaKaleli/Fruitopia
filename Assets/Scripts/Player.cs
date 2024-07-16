using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    [Header("Player Movements")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private int facingDirection;
    private bool isFacingRight;
    private bool canDoubleJump;

    [Header("Collision Checks - Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Collision Checks - Wall")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsWall;
    private bool isWallDetected;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canDoubleJump = true;
        facingDirection = 1;
        isFacingRight = true;
    }

    
    void Update()
    {
        Move();
        CollisionChecks();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpController();
        }

        FlipController();
        AnimationController();
    }

    private void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void JumpController()
    {
        if (isGrounded)
        {
            Jump(1.0f);
            canDoubleJump = true;
        }
        else
        {
            if (canDoubleJump)
            {
                Jump(0.75f);
                canDoubleJump = false;
            }
        }
    }

    private void Jump(float jumpForceMultiplier)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce*jumpForceMultiplier);
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveSpeed * horizontalInput, rb.velocity.y);
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance,whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);
    }

    private void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        isFacingRight = !isFacingRight;
        facingDirection *= -1;
    }

    private void FlipController()
    {
        if (isFacingRight && rb.velocity.x < 0)
            Flip();
        else if (!isFacingRight && rb.velocity.x > 0)
            Flip();
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y, wallCheck.position.z));
        
    }
}
