using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Credits : MonoBehaviour
{
    private UI_FadeEffect fadeEffect;

    [Header("Rect Information")]
    [SerializeField] private RectTransform rectT;
    [SerializeField] private float speed = 200;
    [SerializeField] private float offScreenposition = 2150;

    private void Awake()
    {
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1.5f);
    }


    private void Update()
    {
        rectT.anchoredPosition += Vector2.up * speed * Time.deltaTime;
        if (rectT.anchoredPosition.y > offScreenposition)
            SwitchToMainMenu();
    }

    public void SwitchToMainMenu()
    {
        fadeEffect.ScreenFade(1, 1.5f, LoadTheMainMenuScene);
    }

    public void LoadTheMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
