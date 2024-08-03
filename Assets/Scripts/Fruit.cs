using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        SetRandomLook();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            GameManager.instance.AddFruit();
            Destroy(gameObject);
        }
    }


    //generates random index for visual of fruit.
    private void SetRandomLook()
    {
        int randomIndex = Random.Range(0, 8);
        anim.SetFloat("fruitIndex",randomIndex);
    }



}
