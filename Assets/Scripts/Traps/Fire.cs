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
    private bool hasConnectedSwitcher;



    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        workingTimeCounter = workingTime;
        
    }

    
    void Update()
    {
        workingTimeCounter -= Time.deltaTime;
        if(!hasConnectedSwitcher)
            AutomatedFire();
        
        cc.enabled = isWorking;
        anim.SetBool("isWorking", isWorking);
    }

    private void AutomatedFire()
    {
        if (workingTimeCounter < 0)
        {
            isWorking = !isWorking;
            workingTimeCounter = workingTime;
            
        }    
    }

    public void SetIsWorking(bool status)
    {
        isWorking = status;
    }

    public bool GetHasConnectedSwitcher()
    {
        return this.hasConnectedSwitcher;
    }

    public void SetHasConnectedSwitcher(bool status)
    {
        this.hasConnectedSwitcher = status;
    }
    
}
