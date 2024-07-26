using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Enemy
{

    [Header("Phase Colliders")]
    [SerializeField] private BoxCollider2D bcPhase1;
    [SerializeField] private BoxCollider2D bcPhase2;
    [SerializeField] private BoxCollider2D bcPhase3;

    [Header("Phase Timers")]
    [SerializeField] private float phaseTwoTime;
    private float phaseTwoTimeCounter;
    [SerializeField] private float phaseThreeTime;
    private float phaseThreeTimeCounter;

    private bool phaseTwoActive;
    private bool phaseThreeActive;

    

   

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {

        phaseThreeTimeCounter -= Time.deltaTime;
        phaseTwoTimeCounter -= Time.deltaTime;
        
        
        WalkAround();
        
        PhaseController();
        ColliderController();
        CollisionChecks();
        AnimationController();

    }

    private void PhaseController()
    {
        SetPhaseTwo();
        SetPhaseThree();
    }

    

    private void SetPhaseTwo()
    {
        if (phaseTwoTimeCounter > 0)
        {
            phaseTwoActive = true;
        }
        else
        {
            phaseTwoActive = false;
        }
    }

    private void SetPhaseThree()
    {
        if (phaseThreeTimeCounter > 0)
        {
            phaseThreeActive = true;
        }
        else
        {
            phaseThreeActive = false;
        }
    }

    private void ColliderController()
    {
        if (phaseTwoTimeCounter < 0 && phaseThreeTimeCounter < 0)
        {

            bcPhase1.enabled = true;
            bcPhase2.enabled = true;
            bcPhase3.enabled = true;

        }
        else if (phaseTwoTimeCounter > 0 && phaseThreeTimeCounter < 0)
        {
            bcPhase1.enabled = false;
            bcPhase2.enabled = true;

        }
        else if (phaseTwoTimeCounter > 0 && phaseThreeTimeCounter > 0)
        {

            bcPhase2.enabled = false;
        }
    }


    public override void Damage()
    {

        if(phaseTwoTimeCounter < 0 && phaseThreeTimeCounter < 0)
        {
            phaseTwoTimeCounter = phaseTwoTime;
        }
        else if(!phaseThreeActive && phaseTwoTimeCounter < phaseTwoTime - 0.1f && phaseThreeTimeCounter < 0)
        {
            phaseTwoTimeCounter = phaseTwoTime + phaseThreeTime;
            phaseThreeTimeCounter = phaseThreeTime;
        }
        else if(phaseThreeActive && phaseTwoTimeCounter < (phaseTwoTime + phaseThreeTime) - 0.1f && phaseThreeTimeCounter < phaseThreeTime - 0.1f)
        {
            base.Damage();    
        }
              
    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetBool("phaseTwoActive", phaseTwoActive);
        anim.SetBool("phaseThreeActive", phaseThreeActive);
    }

    

}
