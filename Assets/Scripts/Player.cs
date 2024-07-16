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

    [Header("Collision Checks - Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;
    private bool canDoubleJump;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canDoubleJump = true;
    }

    
    void Update()
    {
        Move();
        CollisionChecks();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpController();
        }



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
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
    }
}
