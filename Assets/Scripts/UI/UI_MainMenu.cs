using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    private UI_FadeEffect fadeEffect;
    [SerializeField] private GameObject[] UI_Elements;
    [SerializeField] private GameObject continueButton;

    private void Awake()
    {
        SetEnableContinueButton();
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1.5f);
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

    private void SetEnableContinueButton()
    {
        bool firstLevelPassed = PlayerPrefs.GetInt("Level2Unlocked", 0) == 1; // if player passed the first level, from now on, player can continue to the level.
        if (firstLevelPassed)
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);
    }

    public void SwitchToContinueLevel()
    {
        fadeEffect.ScreenFade(1, 1.5f, LoadContinueLevel);
    }

    public void LoadContinueLevel()
    {
        int lastContinueLevelIndex = GameManager.instance.lastContinueLevelIndex;
        SceneManager.LoadScene("Level_" + lastContinueLevelIndex);
    }

    

}
