using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
 
    private Animator anim;
    private bool isWorking = true;
    [SerializeField] private float fallTime;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float fallDistance;
    private float fallTimeCounter;
    private bool isFalling;
    [SerializeField] private ParticleSystem dustFX;

    void Start()
    {
        anim = GetComponent<Animator>();
        dustFX.Play();
    }

    
    void Update()
    {
        fallTimeCounter -= Time.deltaTime;
        
        if(isFalling)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,-fallDistance,transform.position.z), fallSpeed * Time.deltaTime);


        anim.SetBool("isWorking", isWorking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            isWorking = false;
            if (dustFX.isPlaying)
                dustFX.Stop();

            StartCoroutine(FallPlatform());
            
        }
    }

    IEnumerator FallPlatform()
    {
        yield return new WaitForSeconds(fallTime);
        isFalling = true;
        Destroy(gameObject, 5f);
    }

}
