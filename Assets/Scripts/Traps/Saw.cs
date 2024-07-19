using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Damage
{

    private Animator anim;

    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float idleTime;
    [SerializeField] private float moveSpeed;
    private int movePointIndex = 1;
    private float idleTimeCounter;
    private bool isWorking;

    void Start()
    {
        transform.position = movePoints[0].position;
        anim = GetComponent<Animator>();
        idleTimeCounter = idleTime;
    }

    // Update is called once per frame
    void Update()
    {
        idleTimeCounter -= Time.deltaTime;

        SawController();

        if (idleTimeCounter < 0)
            isWorking = true;
        


        anim.SetBool("isWorking", isWorking);
    }

    private void SawController()
    {
        if(isWorking)
            transform.position = Vector3.MoveTowards(transform.position, movePoints[movePointIndex].position, moveSpeed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, movePoints[movePointIndex].position) < 0.1f)
        {
            isWorking = false;
            idleTimeCounter = idleTime;
            movePointIndex++;
            Flip();
            if (movePointIndex == movePoints.Length)
                movePointIndex = 0;
        }
    }

    private void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
