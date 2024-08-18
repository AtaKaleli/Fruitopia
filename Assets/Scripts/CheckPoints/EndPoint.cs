using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            player.canBeKnockable = false;
            AudioManager.instance.PlaySFX(2);
            anim.SetTrigger("reached");
            GameManager.instance.LoadNextScene();
        }
    }


}
