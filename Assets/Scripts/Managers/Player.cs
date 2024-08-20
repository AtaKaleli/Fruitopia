using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    

    private DifficultyType gameDifficulty;
    private GameManager gameManager;
   

    private Rigidbody2D rb;
    private Animator anim;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float verticalInput;
    private float horizontalInput;
    private bool walkingSandMudIce;
    private bool canMove = true;
    private bool canDoubleJump = true;
    private bool isFacingRight = true;
    [HideInInspector] public int facingDirection = 1;
    [HideInInspector] public bool canJump = true;
    

    [Header("Knockback Information")]
    [SerializeField] private float knockBackTime;
    [SerializeField] private Vector2 knockBackDirection;
    private bool isKnocked;
    [SerializeField] private float untouchableTime;
    [HideInInspector]
    public bool canBeKnockable = true;
    public bool canBeRespawnable = true;


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


    [Header("Player Visuals")]
    [SerializeField] private GameObject disappearVFXPref;
    [SerializeField] private int skinID;
    //we used animatorOverrideController for skin selection.It allows to override different animations of player
    [SerializeField] private AnimatorOverrideController[] animators;
    

    [Header("FruitDrop")]
    [SerializeField] private GameObject fruitDroppedByPref;


    void Start()
    {
        gameManager = GameManager.instance;
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        UpdateGameDifficulty();
        UpdateSkin();
        StartingPoint();
    }

    

    void Update()
    {
        if (isKnocked)
        {
            return;
        }

        if ((isGrounded || isWallSliding) && !walkingSandMudIce)
        {
            
            canDoubleJump = true;
            canMove = true;
            canJump = true;
        }

        if (canMove && !UI_Ingame.instance.isPaused)
        {
            Move(1.0f);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!canJump)
                return;
            JumpController();
        }

        InputChecks();
        CollisionChecks();
        EnemyChecks();
        WallSlideController();
        FlipController();
        AnimationController();

        
    }

    private void StartingPoint()
    {
        //transform.position = gameManager.respawnPoint.position;
    }
    public void Damage()
    {
        
        

        if(gameDifficulty == DifficultyType.Normal)
        {
            if(gameManager.GetFruitsCollected() <= 0)
            {
                
                Die();
                GameManager.instance.RespawnPlayer(1f);

            }
            else
            {
                Instantiate(fruitDroppedByPref, transform.position, Quaternion.identity);
                gameManager.RemoveFruit();
            }
            
            return;
        }

        if(gameDifficulty == DifficultyType.Hard && canBeRespawnable)
        {
            canBeRespawnable = false;
            Die();
            GameManager.instance.RespawnPlayer(1f);
        }
    }
    private void InputChecks()
    {
         verticalInput = Input.GetAxis("Vertical");
         horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void UpdateGameDifficulty()
    {
        DifficultyManager difficultyManager = DifficultyManager.instance;

        if (difficultyManager != null)
            gameDifficulty = difficultyManager.difficulty;
    }


    public void UpdateSkin()
    {

        SkinManager skinManager = SkinManager.instance;

        if (skinManager == null)
            return;

        anim.runtimeAnimatorController = animators[skinManager.chosenSkinId]; // this allows us to select the desired animations for player
    }

    private void Move(float speedMultiplier)
    {
        
        rb.velocity = new Vector2(horizontalInput * moveSpeed * speedMultiplier, rb.velocity.y);

        
    }

    private void JumpController()
    {
        if (isWallSliding || isGrounded)
        {
            Jump(jumpForce);
        }
        else if (canDoubleJump)
        {
            Jump(jumpForce * 0.75f);
            canDoubleJump = false;
        }
    }

    private void Jump(float jumpForce)
    {
        AudioManager.instance.PlaySFX(3);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
            if (hitCollider.GetComponent<Enemy>() != null)
            {

                Enemy enemy = hitCollider.GetComponent<Enemy>();

                if (enemy.isInvincible)
                {
                    return;
                }

                if (rb.velocity.y <= 0.1f)
                {
                    AudioManager.instance.PlaySFX(2);
                    enemy.Damage();
                    Jump(jumpForce);
                }
            }
            else if (hitCollider.GetComponent<Box>() != null)
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
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput < 0)
        {
            
            isWallSliding = false;
        } // if player pressed S , which means go down, then the wall sliding is interrupted

        else if (isWallDetected && rb.velocity.y < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        else
        {
            isWallSliding = false;
        }
    }

    void FlipController()
    {
        if (isFacingRight && rb.velocity.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && rb.velocity.x > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        isFacingRight = !isFacingRight;
        facingDirection = facingDirection * -1;
    }

    private void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    public void SetCanMove(bool status) => canMove = status;
    
    public void Die()
    {
        AudioManager.instance.PlaySFX(0);
        Destroy(gameObject);
        GameObject newDisappearVFX = Instantiate(disappearVFXPref, transform.position, Quaternion.identity);
    }

    public void KnockBack()
    {
        if (canBeKnockable)
        {
            StartCoroutine(KnockbackController());
        }
    }

    IEnumerator KnockbackController()
    {
        AudioManager.instance.PlaySFX(0);
        isKnocked = true;
        canBeKnockable = false;
        anim.SetTrigger("isKnocked");
        gameManager.ScreenShake(-facingDirection);
        rb.velocity = new Vector2(knockBackDirection.x * -facingDirection, knockBackDirection.y);
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
    private void SandMudIceController(bool walkingStatus, bool canMoveStatus, float newMoveSpeed, float newJumpForce)
    {
        walkingSandMudIce = walkingStatus;
        canMove = canMoveStatus;
        jumpForce = newJumpForce;
        Move(newMoveSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground_Ice"))
        {
            SandMudIceController(true, false, 3f, 15f);
        }
        else if (collision.CompareTag("Ground_Mud"))
        {
            SandMudIceController(true, false, 0f, 10f);
        }
        else if (collision.CompareTag("Ground_Sand"))
        {
            SandMudIceController(true, false, 0.4f, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground_Ice" || collision.tag == "Ground_Mud" || collision.tag == "Ground_Sand")
        {
            SandMudIceController(false, true, 1f, 15f);
        }
    }





}
