using UnityEngine;

public class Turtle : Enemy
{
    [Header("Spike On-Off Time")]
    [SerializeField] private float spikeTime;
    private float spikeTimeCounter;
    private bool isSpikesOn = true;


    protected override void Start()
    {
        base.Start();
        spikeTimeCounter = spikeTime;

    }

    void Update()
    {
        spikeTimeCounter -= Time.deltaTime;




        CollisionChecks();
        SpikeController();
        anim.SetBool("isSpikesOn", isSpikesOn);
    }

    //If I want to change two states between a spesific time, them whenever timeCounter goes below 0, then make it equal to specific time, and change the state to opposite.
    private void SpikeController()
    {
        if (spikeTimeCounter < 0)
        {
            isSpikesOn = !isSpikesOn;
            spikeTimeCounter = spikeTime;
        }

        if (isSpikesOn)
            isInvincible = true;
        else
            isInvincible = false;
    }


}
