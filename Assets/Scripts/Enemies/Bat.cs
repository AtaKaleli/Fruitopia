using UnityEngine;


public class Bat : Enemy
{

    [Header("Player Checks")]
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerTransform;
    private bool isPlayerDetected;

    [Header("Idle Point and Timers Checks")]
    [SerializeField] private Transform[] movePoints;
    private int movePointIndex = 0;
    [SerializeField] private float aggressiveTime;
    private float aggressiveTimeCounter;
    

    private bool canBeAggressive = true;
    



    protected override void Start()
    {
        base.Start();
        transform.position = new Vector2(movePoints[0].position.x, movePoints[0].position.y - 0.6f);
        playerTransform = FindObjectOfType<Player>().transform;

    }

    void Update()
    {
        idleTimeCounter -= Time.deltaTime;
        


        MovementController();

        CollisionChecks();
        anim.SetBool("isAggressive", isAggressive);
    }



    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.OverlapCircle(playerCheck.position, playerDetectionDistance, whatIsPlayer);

    }


    private void MovementController()
    {
        if (isPlayerDetected && canBeAggressive && idleTimeCounter < 0)
        {
            canBeAggressive = false;
            aggressiveTimeCounter = aggressiveTime;
            MovePointIndexController();
        }

        if (aggressiveTimeCounter > 0)
        {
            FlipController();
            
            isAggressive = true;
            aggressiveTimeCounter -= Time.deltaTime;
            if (canMove)
                ChangePosition(2f,"Player");


        }
        else
        {
            if (Vector2.Distance(transform.position, movePoints[movePointIndex].position) > 0.8f)
                ChangePosition(1f,"Idle");
            else
            {
                canBeAggressive = true;
                isAggressive = false;
            }
        }


    }

    //after every attack, change next idle point of bat
    private void MovePointIndexController()
    {
        movePointIndex++;
        if (movePointIndex == movePoints.Length)
            movePointIndex = 0;
    }

    //flip the bat based on player's x coordinate
    private void FlipController()
    {
        if (transform.position.x - playerTransform.position.x < 0 && facingDirection == -1)
        {
            Flip();
        }
        else if (transform.position.x - playerTransform.position.x > 0 && facingDirection == 1)
        {
            Flip();
        }
    }

    //set and restrict movements and timers from animations.
    public void ClearCanMove()
    {
        canMove = false;
    }

    public void SetCanMove()
    {
        canMove = true;
        
    }

    public void SetIdleTimeCounter()
    {
        idleTimeCounter = idleTime;
    }
    
    
    //
    private void ChangePosition(float multiplier, string position)
    {
        if(position == "Player")
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * multiplier * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, movePoints[movePointIndex].position, moveSpeed * multiplier * Time.deltaTime);
    }

    


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheck.position, playerDetectionDistance);
    }


    //If bat hit player, directly return to next idle point
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.GetComponent<Player>() != null)
        {
            aggressiveTimeCounter = -1;
        }
    }

    private void UpdatePlayerTransform()
    {
        playerTransform = FindObjectOfType<Player>().transform;
    }
    private void OnEnable()
    {
        GameManager.OnPlayerRespawn += UpdatePlayerTransform;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerRespawn -= UpdatePlayerTransform;
    }
}
