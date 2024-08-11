using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private Animator anim;
    private bool canBeActivated = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null && canBeActivated)
        {
            AudioManager.instance.PlaySFX(11);

            anim.SetTrigger("hitCheckPoint");
            GameManager.instance.UpdateRespawnPoint(transform);
            canBeActivated = false;
        }
    }
}
