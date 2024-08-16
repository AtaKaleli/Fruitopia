
using UnityEngine;

public class Ghost : Enemy
{
    [SerializeField] private float alphaMultiplier;
    private SpriteRenderer sr;

    [Header("Player Checks")]
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform idlePoint;
    private bool isPlayerDetected;


    protected override void Start()
    {
        base.Start();
        transform.position = idlePoint.position;
        playerTransform = FindObjectOfType<Player>().transform;
        sr = GetComponent<SpriteRenderer>();
        

        
    }

    void Update()
    {

        MovementController();
        CollisionChecks();
        
    }

    

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.OverlapCircle(playerCheck.position, playerDetectionDistance, whatIsPlayer);
    }

    
    private void ColorAlphaController()
    {
        Color tmp = sr.color;
        tmp.a = alphaMultiplier * Vector2.Distance(transform.position, idlePoint.position) * Time.deltaTime;
        sr.color = tmp;
    }

    private void MovementController()
    {
        ColorAlphaController();

        if(playerTransform == null)
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoint.position, moveSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, idlePoint.position) < 0.1f)
        {
            isAggressive = true;
        }

        if (isPlayerDetected && isAggressive)
        {
            FlipController();
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed  * 2 * Time.deltaTime);
        }
        else if((isPlayerDetected && !isAggressive) || !isPlayerDetected)
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoint.position, moveSpeed * Time.deltaTime);
        }
        
    }

    private void FlipController()
    {
        if (playerTransform == null)
            return;

        if (transform.position.x - playerTransform.position.x < 0 && facingDirection == -1)
        {
            Flip();
        }
        else if (transform.position.x - playerTransform.position.x > 0 && facingDirection == 1)
        {
            Flip();
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheck.position, playerDetectionDistance);
    }

    //If ghost hit player, directly return to next idle point
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.GetComponent<Player>() != null)
        {
            isAggressive = false;
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
