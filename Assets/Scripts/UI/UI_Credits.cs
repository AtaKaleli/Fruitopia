using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Credits : MonoBehaviour
{
    [SerializeField] private RectTransform rectT;
    [SerializeField] private float speed = 200;
    [SerializeField] private float offScreenposition = 2150;


    private void Update()
    {
        rectT.anchoredPosition += Vector2.up * speed * Time.deltaTime;
        if (rectT.anchoredPosition.y > offScreenposition)
            GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
