using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Damage
{
    private Rigidbody2D rb;
    private float moveSpeedX;
    private float moveSpeedY;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveSpeedX, moveSpeedY);
    }
    
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        moveSpeedX = xVelocity; 
        moveSpeedY = yVelocity; 
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.tag != "EndPoint")
            Destroy(gameObject);

           
            
            
    }
}
