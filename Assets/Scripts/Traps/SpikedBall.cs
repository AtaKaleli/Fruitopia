using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : Damage
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2 directionForce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(directionForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
