using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    private BoxCollider2D bc;
    private Animator anim;

    [SerializeField] private float idleTime;
    [SerializeField] private float activeTime;
    [SerializeField] private float pushUpwardForce;
    [SerializeField] private Vector2 bcSize;
    private float idleTimeCounter;
    private bool isWorking;

    [SerializeField] private ParticleSystem dustFX;
    

    void Start()
    {
        dustFX = GetComponentInChildren<ParticleSystem>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        idleTimeCounter = idleTime;

        bc.size = bcSize;
        bc.offset = new Vector2(0f, (bcSize.y) / 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        idleTimeCounter -= Time.deltaTime;

        
        if (idleTimeCounter < 0)
        {
            bc.enabled = true;
            isWorking = true;
            if(!dustFX.isPlaying)
                dustFX.Play();
        }
        else
        {
            isWorking = false;
            bc.enabled = false;
            if(dustFX.isPlaying)
                dustFX.Stop();
        }


        FanController();
        anim.SetBool("isWorking", isWorking);

    }

    private void FanController()
    {
        if (Mathf.Abs(idleTimeCounter) > activeTime)
            idleTimeCounter = idleTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(playerRb.velocity.x, pushUpwardForce);
        }
    }


}
