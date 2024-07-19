using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private Animator anim;

    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float idleTime;
    private float idleTimeCounter;
    private bool isWorking;
    private int movePointIndex = 1;

    void Start()
    {
        anim = GetComponent<Animator>();
        idleTimeCounter = idleTime;
        transform.position = movePoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        idleTimeCounter -= Time.deltaTime;

        PlatformController();
        
        if (idleTimeCounter < 0)
            isWorking = true;

        anim.SetBool("isWorking", isWorking);
    }

    private void PlatformController()
    {
        if(isWorking)
            transform.position = Vector3.MoveTowards(transform.position, movePoints[movePointIndex].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[movePointIndex].position) < 0.1f)
        {
            isWorking = false;
            idleTimeCounter = idleTime;
            movePointIndex++;

            if (movePointIndex > 1)
                movePointIndex = 0;
        }
    }

    // set the player as a child of platform when s/he is on it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            collision.transform.SetParent(transform);  
            
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            collision.transform.SetParent(null);
    }
}
