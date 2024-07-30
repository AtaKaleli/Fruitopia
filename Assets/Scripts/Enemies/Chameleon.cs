using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : Enemy
{

    [SerializeField] private BoxCollider2D tonqueBC;

    [Header("Player Detection")]
    private RaycastHit2D playerDetection;
    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    private bool isPlayerDetected;

    [Header("AggressiveMode Timers")]
    [SerializeField] private float aggressiveTime;
    private float aggressiveTimeCounter;
    

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        aggressiveTimeCounter -= Time.deltaTime;



        EnemyStateController();
        CollisionChecks();
        AnimationController();

        anim.SetBool("isAggressive", isAggressive);
    }
    private void EnemyStateController()
    {
        if (!playerDetection && aggressiveTimeCounter < 0)
        {
            isAggressive = false;
            canMove = true;
            WalkAround();
        }
        
    }

    public void MakeTonqueEnable()
    {
        tonqueBC.enabled = true;
    }
    public void MakeTonqueDisable()
    {
        tonqueBC.enabled = false;
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        playerDetection = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, playerDetectionDistance, whatIsPlayer);

        if (playerDetection)
        {
            aggressiveTimeCounter = aggressiveTime;
            isAggressive = true;
        }
        

    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + playerDetection.distance * facingDirection, transform.position.y));
    }
}
