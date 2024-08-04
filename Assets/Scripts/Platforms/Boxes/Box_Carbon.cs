using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Carbon : Box
{
    [SerializeField] private GameObject fruitPref;
    [SerializeField] private int pushForce;

    protected override void Start()
    {
        base.Start();
        hitPoint = 5;
    }


    public override void Damage()
    {
        base.Damage();

        for (int i = 0; i < 2; i++)
        {
            int xOffset = Random.Range(-pushForce, pushForce);
            int yOffset = Random.Range(pushForce/2, pushForce);
            GameObject newFruit = Instantiate(fruitPref, new Vector2(transform.position.x, transform.position.y + 0.75f), Quaternion.identity);
            Rigidbody2D prefRb = newFruit.GetComponent<Rigidbody2D>();
            prefRb.AddForce(new Vector2(xOffset, yOffset), ForceMode2D.Impulse);
        } 
    }
}
