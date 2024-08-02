using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;



    [Header("Player Forces")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool walkingSpecialGround;

    [Header("Knockback Information")]
    [SerializeField] private float knockBackTime;
    [SerializeField] private Vector2 knockBackDirection;
    private bool isKnocked;
    [SerializeField] private float untouchableTime;
    private bool canBeKnockable = true;


    private bool canMove = true;
    private bool canDoubleJump = true;
    private bool isFacingRight = true;
    [HideInInspector]public int facingDirection = 1;

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

    [Header("Collision Checks - EnemyKill")]
    [SerializeField] private Transform enemyKillCheck;
    [SerializeField] private float enemyKillCheckDistance;
    

    [Header("Collision Checks - Box")]
    [SerializeField] private Transform boxHitCheck;
    [SerializeField] private float boxHitCheckDistance;
    
   



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        if (isKnocked)
            return;

        if ((isGrounded || isWallSliding) && !walkingSpecialGround)
        {
            canDoubleJump = true;
            canMove = true;

        }

        if (canMove)
        {
            Move(1.0f);
        }

        CollisionChecks();
        EnemyChecks();
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

    private void EnemyChecks()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(enemyKillCheck.position, enemyKillCheckDistance);
        Collider2D[] boxColliders = Physics2D.OverlapCircleAll(boxHitCheck.position, boxHitCheckDistance);

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.GetComponent<Enemy>() != null)
            {
                
                Enemy enemy = hitCollider.GetComponent<Enemy>();
               
                if (enemy.isInvincible)
                    return;
                
                if(rb.velocity.y < 0)
                {
                    enemy.Damage();
                    Jump(jumpForce);
                }
                
            }
            else if(hitCollider.GetComponent<Box>() != null)
            {
                Box box = hitCollider.GetComponent<Box>();

                if (rb.velocity.y < 0)
                {
                    
                    box.Damage();
                    Jump(jumpForce * 0.8f);
                }
            }
            
        }

        foreach (var hitCollider in boxColliders)
        {
            if (hitCollider.GetComponent<Box>() != null)
            {
                Box box = hitCollider.GetComponent<Box>();

                if (rb.velocity.y >= 0)
                {
                    
                    box.Damage();
                    Jump(-jumpForce * 0.8f);
                }
            }
        }
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

    private void Move(float speedMultiplier)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed * speedMultiplier, rb.velocity.y);
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

    public void SetCanMove(bool status)
    {
        canMove = status;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void KnockBack()
    {
        if(canBeKnockable)
            StartCoroutine(KnockbackController());
    }

    IEnumerator KnockbackController()
    {
        isKnocked = true;
        canBeKnockable = false;
        anim.SetTrigger("isKnocked");
        rb.velocity = new Vector2(knockBackDirection.x * -facingDirection , knockBackDirection.y);
        yield return new WaitForSeconds(knockBackTime);
        isKnocked = false;
        canBeKnockable = true;
    }

    
   

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckRadius, groundCheck.position.z));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckRadius * facingDirection, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawWireSphere(enemyKillCheck.position, enemyKillCheckDistance);
        Gizmos.DrawWireSphere(boxHitCheck.position, boxHitCheckDistance);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Ground_Ice>() != null)
        {
            walkingSpecialGround = true;
            canMove = false;
            Move(3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Ground_Ice>() != null)
        {
            walkingSpecialGround = false;
            canMove = true;
        }
    }

}
