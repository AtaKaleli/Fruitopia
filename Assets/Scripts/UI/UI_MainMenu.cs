using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    private UI_FadeEffect fadeEffect;
    [SerializeField] private GameObject[] UI_Elements;

    private void Awake()
    {
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1.5f);
    }


    public void SwitchToNewLevel()
    {
        fadeEffect.ScreenFade(1, 1.5f, LoadTheLevelScene);
    }

    private void LoadTheLevelScene()
    {
        SceneManager.LoadScene("Level1");
    }

    public void SwitchToCredits()
    {
        fadeEffect.ScreenFade(1, 1.5f, LoadTheCreditsScene);
    }

    private void LoadTheCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void SwicthToUI(GameObject UI_Element)
    {
        foreach (GameObject ui in UI_Elements)
        {
            ui.SetActive(false);
        }
        UI_Element.SetActive(true);
    }
    
}
