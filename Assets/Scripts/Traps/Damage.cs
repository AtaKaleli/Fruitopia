using UnityEngine;

public class Damage : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            player.KnockBack();
        }


    }
}
