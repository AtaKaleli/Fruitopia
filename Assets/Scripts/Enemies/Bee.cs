using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Plant
{
    [SerializeField] private Transform playerCheck;
    private bool isPlayerDetected;
    [SerializeField] private Transform[] movePoints;
    private int movePointIndex = 1;

    protected override void Start()
    {
        base.Start();
        transform.position = movePoints[0].position;
    }

    void Update()
    {

        attackTimeCounter -= Time.deltaTime;

        if(!isAggressive)
            IdleController();
        CollisionChecks();
        anim.SetBool("isAggressive", isAggressive);
    }

    private void IdleController()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoints[movePointIndex].position, moveSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, movePoints[movePointIndex].position) < 0.1f)
        {
            movePointIndex++;
            if(movePointIndex == movePoints.Length)
            {
                movePointIndex = 0;
            }
        }
    }
    

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.OverlapCircle(playerCheck.position, playerDetectionDistance, whatIsPlayer);


        if (isPlayerDetected && attackTimeCounter < 0)
        {
            isAggressive = true;

        }


    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheck.position, playerDetectionDistance);
    }

    protected override void Attack()
    {
        base.Attack();

        GameObject newBullet2 = Instantiate(bulletPref, bulletOrigin.position, Quaternion.identity);
        newBullet2.GetComponent<Bullet>().SetVelocity(0, bulletSpeedY);

        GameObject newBullet3 = Instantiate(bulletPref, bulletOrigin.position, Quaternion.identity);
        newBullet3.GetComponent<Bullet>().SetVelocity(-bulletSpeedX * facingDirection, bulletSpeedY);

    }
}
