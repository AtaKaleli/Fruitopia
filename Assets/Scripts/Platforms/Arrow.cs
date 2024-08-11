using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Vector2 arrowForce;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            AudioManager.instance.PlaySFX(12);

            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            collision.GetComponent<Player>().SetCanMove(false);
            playerRb.velocity = arrowForce;
            anim.SetTrigger("gotHit");
        }
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
