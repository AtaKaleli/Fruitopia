using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatbird : Enemy
{
    [Header("Player Detection")]
    [SerializeField] private Transform playerCheck;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float playerDetectionDistance;
    private bool isPlayerDetected;
    private bool canBeAggressive;

    private bool isFalling;

    [Header("Move Points")]
    [SerializeField] private Transform idlePoint;
    [SerializeField] private Transform fallPoint;

    protected override void Start()
    {
        base.Start();
        transform.position = idlePoint.position;
       
    }

    void Update()
    {
        idleTimeCounter -= Time.deltaTime;

        MovementController();
        CollisionChecks();


        anim.SetBool("isFalling", isFalling);
    }

    private void MovementController()
    {
        //if detects player, it cant detect until it goes back to idlePoit after falling to ground
        if (isPlayerDetected && canBeAggressive)
        {
            isFalling = true;
            canBeAggressive = false;
            
        }

        if (isFalling)
        {
            transform.position = Vector2.MoveTowards(transform.position, fallPoint.position, moveSpeed * 2 * Time.deltaTime);
            
            //if fatbird reached the fallPoint, then make it move to idle point slower than the fall in 'else' part
            if (Vector2.Distance(transform.position, fallPoint.position) < 0.1f)
            {
                isFalling = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoint.position, moveSpeed * 0.5f * Time.deltaTime);
            
            if (Vector2.Distance(transform.position, idlePoint.position) < 0.1f)
            {
                canBeAggressive = true;
            }
        }
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        
        isPlayerDetected = Physics2D.OverlapCircle(playerCheck.position, playerDetectionDistance, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.DrawWireSphere(playerCheck.position, playerDetectionDistance);
    }

    
}
