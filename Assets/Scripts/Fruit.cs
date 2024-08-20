using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    [SerializeField] private GameObject collectedVFXPref;
    [SerializeField] protected bool hasNoGravity;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        SetRandomLook();

        if (hasNoGravity) // if fruit has no gravity, this means it is not instantiated in the box.
        {
            rb.gravityScale = 0;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            AudioManager.instance.PlaySFX(8);
            GameManager.instance.AddFruit();
            Destroy(gameObject);

            //add visual effect after destroying the fruit
            GameObject newCollectedVFX = Instantiate(collectedVFXPref, transform.position, Quaternion.identity);
        }
    }


    //generates random index for visual of fruit.
    private void SetRandomLook()
    {
        int randomIndex = Random.Range(0, 8);
        anim.SetFloat("fruitIndex",randomIndex);
    }



}
