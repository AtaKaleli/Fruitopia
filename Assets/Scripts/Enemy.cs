using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damage
{
    protected Animator anim;
    protected Rigidbody2D rb;

    [Header("Move Info")]
    [SerializeField] protected float moveSpeed;
    protected int facingDirection = -1;
    [SerializeField] protected float idleTime;
    protected float idleTimeCounter;
    protected bool isAggressive;
    protected bool canMove = true;


    [Header("Collision Checks - Wall")]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsWall;
    protected bool isWallDetected;

    [Header("Collision Checks - Ground")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    protected bool isGrounded;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
    }

    protected virtual void WalkAround()
    {
        idleTimeCounter -= Time.deltaTime;

        if (isWallDetected || !isGrounded)
        {
            idleTimeCounter = idleTime;
            Flip();
        }

        if (idleTimeCounter < 0 && canMove)
            Move(moveSpeed * facingDirection, rb.velocity.y);
        else
            Move(0, 0);
    }

   

    protected virtual void Move(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    protected virtual void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDirection = facingDirection * -1;
    }

    protected virtual void CollisionChecks()
    {
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
    }

    public void DestroyMe()
    {
        rb.velocity = new Vector2(0, 0);
        Destroy(gameObject);
    }

    public virtual void Damage()
    {
        anim.SetTrigger("gotHit");   
    }

}
