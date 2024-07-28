using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{

    [SerializeField] private float shellHitSpeed;
    private bool gotDamage;
    private bool isBehindWallDetected;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if(!gotDamage)
            WalkAround();
        else if (isWallDetected || isBehindWallDetected)
        {
            anim.SetTrigger("wallHit");
            Destroy(gameObject, 0.5f);
        }


        CollisionChecks();
        AnimationController();
    }


    public override void Damage()
    {
        gotDamage = true;
        base.Damage();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gotDamage)
            base.OnTriggerEnter2D(collision);
        else
        {
            // psuh the shell based on player's push dircetion
            Player player = collision.GetComponent<Player>();
            if (player == null)
                return;
            Move(shellHitSpeed * player.facingDirection, rb.velocity.y);
        }        
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isBehindWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDirection, wallCheckDistance, whatIsWall);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * -facingDirection, wallCheck.position.y, wallCheck.position.z));

    }
}
