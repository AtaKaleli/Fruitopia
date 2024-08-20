using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDroppedByPlayer : Fruit
{


    private bool canPickUp;
    private CircleCollider2D cc;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Color transparentColor;
    [SerializeField] private float[] blinkTimers;


    protected override void Start()
    {
        cc = GetComponentsInChildren<CircleCollider2D>()[1];
                                
        cc.enabled = false;
        hasNoGravity = true;
        base.Start();
        StartCoroutine(BlinkCoroutine());
        DestroyMe();

    }


    private void Update()
    {
        transform.position += new Vector3(direction.x, direction.y) * Time.deltaTime;
    }

    private IEnumerator BlinkCoroutine()
    {
        anim.speed = 0;

        for (int i = 0; i < blinkTimers.Length; i++)
        {
            sr.color = transparentColor;
            direction.x = direction.x * -1;
            yield return new WaitForSeconds(blinkTimers[i]);
            sr.color = Color.white;
            direction.x = direction.x * -1;
            yield return new WaitForSeconds(blinkTimers[i]);
        }

        direction.x = 0;
        anim.speed = 1;
        canPickUp = true;
    }



    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canPickUp)
            return;

        base.OnTriggerEnter2D(collision);
    }

    private void DestroyMe()
    {
        Destroy(gameObject, 40);
    }

    
}
