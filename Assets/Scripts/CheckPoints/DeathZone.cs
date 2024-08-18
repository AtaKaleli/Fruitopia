using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            player.Damage();
            player.Die();
            DifficultyManager difficultyManager = DifficultyManager.instance;
            if(difficultyManager.difficulty == DifficultyType.Easy)
                GameManager.instance.RespawnPlayer(1f);
        }


    }
}
