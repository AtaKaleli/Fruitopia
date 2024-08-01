using UnityEngine;

public class Plant : Enemy
{

    [Header("Player Detection")]
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected float playerDetectionDistance;
    protected RaycastHit2D playerDetection;

    [Header("Attack Information")]
    [SerializeField] protected GameObject bulletPref;
    [SerializeField] protected Transform bulletOrigin;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float attackTime;
    [SerializeField] private bool isFacingRight;
    protected float attackTimeCounter;



    protected override void Start()
    {
        base.Start();

        if (isFacingRight)
        {
            Flip();
        }

    }

    void Update()
    {

        attackTimeCounter -= Time.deltaTime;

        


        CollisionChecks();
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

    protected void SetAttackTimer()
    {
        attackTimeCounter = attackTime;
        isAggressive = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + playerDetection.distance * facingDirection, transform.position.y));
    }


    protected void Attack()
    {

        GameObject newBullet = Instantiate(bulletPref, bulletOrigin.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetVelocity(bulletSpeed * facingDirection);

    }



}
