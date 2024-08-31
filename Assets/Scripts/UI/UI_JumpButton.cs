using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_JumpButton : MonoBehaviour, IPointerDownHandler
{
    private Player player;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.JumpController();
    }

    public void UpdatePlayerRef(Player newPlayer)
    {
        player = newPlayer;
    }
}
