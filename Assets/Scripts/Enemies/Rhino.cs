using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Enemy
{
    [Header("Player Detection")]
    private RaycastHit2D playerDetection;
    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Aggressive Mode")]
    [SerializeField] private float aggressiveTime;
    private float aggressiveTimeCounter;

    [SerializeField] private float aggressiveMoveSpeed;
    [SerializeField] private float normalMoveSpeed;

    private bool isShocked;

    protected override void Start()
    {
        base.Start();
        isInvincible = true;
        
    }

    void Update()
    {
        aggressiveTimeCounter -= Time.deltaTime;


        PhaseController();
        WalkAround();
        CollisionChecks();
        AnimationController();
    }


    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        playerDetection = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, playerDetectionDistance, whatIsPlayer);

        if (playerDetection)
        {
            aggressiveTimeCounter = aggressiveTime;
            moveSpeed = aggressiveMoveSpeed;
        }
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + playerDetection.distance * facingDirection, transform.position.y));
    }

    private void PhaseController()
    {
        if(!playerDetection && aggressiveTimeCounter < 0)
        { 
            moveSpeed = normalMoveSpeed;
        }

        else if(!playerDetection && aggressiveTimeCounter > 0 && isWallDetected)
        {
            isShocked = true;
            isInvincible = false;
        }

    }

    public void BackToNormal()
    {
        
        isShocked = false;
        isInvincible = true;
    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetBool("isShocked", isShocked);
    }
}
