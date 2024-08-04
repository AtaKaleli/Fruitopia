using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private GameObject collectedVFXPref;
    [SerializeField] private bool hasNoGravity;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        SetRandomLook();

        if (hasNoGravity) // if fruit has no gravity, this means it is not instantiated in the box.
        {
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
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
