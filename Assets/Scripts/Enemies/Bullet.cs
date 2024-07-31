using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Damage
{
    private Rigidbody2D rb;
    private float moveSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, 0);
    }
    
    public void SetVelocity(float xVelocity)
    {
        moveSpeed = xVelocity; 
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Destroy(gameObject);
    }
}
