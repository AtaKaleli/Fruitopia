using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwitcher : MonoBehaviour
{

    private Animator anim;

    [SerializeField] private Fire fire;
    [SerializeField] private float turnTime;
    private float turnTimeCounter;
    private bool canBePressable;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (fire != null)
            fire.SetHasConnectedSwitcher(true);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        turnTimeCounter -= Time.deltaTime;
        if (turnTimeCounter > 0)
            canBePressable = false;
        else
            canBePressable = true;



        anim.SetBool("canBePressable", canBePressable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.fire.GetHasConnectedSwitcher())
        {
            if (collision.GetComponent<Player>() != null)
            {
                if (canBePressable)
                {
                    turnTimeCounter = turnTime;
                    StartCoroutine(SetFireWorking());
                }
            }
        }
        
    }

    IEnumerator SetFireWorking()
    {
        fire.SetIsWorking(false);
        yield return new WaitForSeconds(turnTime);
        fire.SetIsWorking(true);

    }
}
