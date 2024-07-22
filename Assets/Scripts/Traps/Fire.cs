using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Damage
{
    private Animator anim;
    private CapsuleCollider2D cc;

    private bool isWorking = true;
    [SerializeField] private float workingTime;
    private float workingTimeCounter;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        workingTimeCounter = workingTime;
        
    }

    
    void Update()
    {
        workingTimeCounter -= Time.deltaTime;
        FireSwitcher();
        anim.SetBool("isWorking", isWorking);
    }

    private void FireSwitcher()
    {
        if (workingTimeCounter < 0)
        {
            isWorking = !isWorking;
            workingTimeCounter = workingTime;
            cc.enabled = isWorking;
        }    
    }
}
