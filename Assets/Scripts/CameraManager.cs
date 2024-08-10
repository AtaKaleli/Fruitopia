using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject myCamera;

    private void Start()
    {
        myCamera.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            myCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            myCamera.SetActive(false);
        }
    }
}
