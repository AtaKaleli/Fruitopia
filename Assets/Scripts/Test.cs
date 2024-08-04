using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] private GameObject testPref;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            
            int xOffset = Random.Range(-6, 6);
            int yOffset = Random.Range(3, 6);

            
            GameObject newTest = Instantiate(testPref, new Vector2(transform.position.x,transform.position.y + 0.75f), Quaternion.identity);
            Rigidbody2D prefRb = newTest.GetComponent<Rigidbody2D>();
            prefRb.AddForce(new Vector2(xOffset, yOffset),ForceMode2D.Impulse);
        }
    }
}
