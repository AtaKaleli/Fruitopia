using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    protected Animator anim;
    protected int hitPoint;
    
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Damage()
    {
        hitPoint--;
        anim.SetTrigger("gotHit");

        if (hitPoint == 0)
        {
            DestroyMe();
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject,0.15f);
    }
}
