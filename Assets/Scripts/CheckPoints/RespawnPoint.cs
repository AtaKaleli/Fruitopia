using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        AudioManager.instance.PlayRandomBGM();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            anim.SetTrigger("exitRespawnPoint");
        }
    }
}
