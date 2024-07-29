using UnityEngine;

public class Bunny : Enemy
{

    [Header("Jump Timer")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    private float jumpTimeCounter;

    [Header("Player Detection")]
    private RaycastHit2D playerDetection;
    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    private bool isPlayerDetected;



    protected override void Start()
    {
        base.Start();


    }



    void Update()
    {



        if (isGrounded)
        {
            jumpTimeCounter -= Time.deltaTime;
            idleTimeCounter -= Time.deltaTime;
        }





        JumpController();
        CollisionChecks();
        AnimationController();

    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public void Jump()
    {
        Move(0, jumpForce);
    }

    private void JumpController()
    {
        if (!isPlayerDetected)
            WalkAround();
        else
        {

            if (jumpTimeCounter < 0)
            {
                isPlayerDetected = false;
            }

            if (idleTimeCounter < 0)
            {
                Jump();
                idleTimeCounter = 0.25f;
            }
        }
    }



    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        playerDetection = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, playerDetectionDistance, whatIsPlayer);

        if (playerDetection)
        {
            isPlayerDetected = true;
            jumpTimeCounter = jumpTime;

        }

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + playerDetection.distance * facingDirection, transform.position.y));
    }
}
