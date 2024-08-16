using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Vector2 arrowForce;
    [SerializeField] private GameObject arrowPref;
    private Vector2 platformPosition;
    private SpriteRenderer sr;
    private CircleCollider2D cc;


    private void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();

       
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            
            AudioManager.instance.PlaySFX(12);
            platformPosition = transform.position;
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            collision.GetComponent<Player>().SetCanMove(false);
            playerRb.velocity = arrowForce;
            anim.SetTrigger("gotHit");
            
            
        }
   
    }


    private IEnumerator RespawnArrow()
    {
        yield return new WaitForSeconds(3f);
        sr.enabled = true;
        cc.enabled = true;
    }


    private void DestroyMe()
    {
        sr.enabled = false;
        cc.enabled = false;
        StartCoroutine(RespawnArrow());
        
    }
}
