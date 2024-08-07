using UnityEngine;

public class Trunk : Plant
{

    


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

    

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + playerDetection.distance * facingDirection, transform.position.y));
    }

    
}
