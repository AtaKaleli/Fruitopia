using UnityEngine;

public class Trunk : Enemy
{

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float playerDetectionDistance;
    private RaycastHit2D playerDetection;

    [Header("Attack Information")]
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackTime;
    private float attackTimeCounter;


    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {


        attackTimeCounter -= Time.deltaTime;

        MovementController();
        WalkAround();
        CollisionChecks();
        AnimationController();
    }

    private void MovementController()
    {
        if (attackTimeCounter > 0)
        {
            canMove = false;
        }
        else if (!playerDetection && attackTimeCounter < -1.0f)
        {
            canMove = true;
        }
    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetBool("isAggressive", isAggressive);
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        playerDetection = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, playerDetectionDistance, whatIsPlayer);


        if (playerDetection && attackTimeCounter < 0)
        {
            isAggressive = true;
        }
    }

    private void SetAttackTimer()
    {
        attackTimeCounter = attackTime;
        isAggressive = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + playerDetection.distance * facingDirection, transform.position.y));
    }

    private void Attack()
    {
        GameObject newBullet = Instantiate(bulletPref, bulletOrigin.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetVelocity(bulletSpeed * facingDirection);
    }
}
