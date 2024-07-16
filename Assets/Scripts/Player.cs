using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private bool canDoubleJump;
    private bool isFacingRight;
    private int facingDirection;
    private bool canMove;

    [Header("Player Forces")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [Header("Collision Checks - Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Collision Checks - Wall")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius;
    [SerializeField] private LayerMask whatIsWall;
    private bool isWallDetected;
    private bool isWallSliding;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canMove = true;
        canDoubleJump = true;
        isFacingRight = true;
        facingDirection = 1;
    }


    void Update()
    {

        if (isGrounded)
        {
            canDoubleJump = true;
            canMove = true;
        }

        
        Move();

        CollisionChecks();
        AnimationController();
        FlipController();
        WallSlideController();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpController();

        }



    }

    private void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallSliding", isWallSliding);

    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckRadius, whatIsWall);

        

    }

    

    private void WallSlideController()
    {
        if (isWallDetected && rb.velocity.y < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        else
            isWallSliding = false;
    }



    private void JumpController()
    {
        if (isWallSliding || isGrounded)
        {
            Jump(jumpForce);
            canDoubleJump = true;
        }
        else
        {
            if (canDoubleJump)
            {
                Jump(jumpForce * 0.75f);
                canDoubleJump = false;
            }
        }
    }

    private void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Move()
    {

        if (canMove)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }

    }

    void FlipController()
    {
        if (isFacingRight && rb.velocity.x < 0)
            Flip();
        else if (!isFacingRight && rb.velocity.x > 0)
            Flip();
    }
    void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        isFacingRight = !isFacingRight;
        facingDirection = facingDirection * -1;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckRadius, groundCheck.position.z));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckRadius * facingDirection, wallCheck.position.y, wallCheck.position.z));
    }
}
