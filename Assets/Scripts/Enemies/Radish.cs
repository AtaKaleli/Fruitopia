using UnityEngine;

public class Radish : Enemy
{

    [Header("Collision Checks - UpperGround")]
    [SerializeField] private float upperGroundCheckDistance;
    [SerializeField] private Transform upperGroundCheck;
    private bool isUpperGrounded;

    [Header("Fly Time")]
    [SerializeField] private float groundTime;
    private float groundTimeCounter;

    [SerializeField] private float flyForce;
    private bool isFlying = true;


    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (isGrounded)
            groundTimeCounter -= Time.deltaTime;


        MovementController();
        CollisionChecks();
        AnimationController();

    }


    private void MovementController()
    {
        if (!isFlying)
        {
            rb.gravityScale = 20;
            WalkAround();
        }
        else
        {
            rb.gravityScale = 0;
            Fly();
        }

        if(isGrounded && groundTimeCounter < 0 && !isUpperGrounded)
        {
            isFlying = true;
        }

    }

    private void Fly()
    {
        if (!isUpperGrounded)
            Move(0, flyForce);
        else
            Move(0, 0);
    }

    public override void Damage()
    {
        if (isFlying)
        {
            isFlying = false;
            groundTimeCounter = groundTime;
        }
        else
        {
            base.Damage();
        }
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isUpperGrounded = Physics2D.Raycast(upperGroundCheck.position, Vector2.up, upperGroundCheckDistance, whatIsGround);
        
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(upperGroundCheck.position, new Vector2(upperGroundCheck.position.x, upperGroundCheck.position.y + upperGroundCheckDistance));
        
    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetBool("isFlying", isFlying);
    }
}
