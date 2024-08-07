using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Ingame : MonoBehaviour
{
    public static UI_Ingame instance;
    public UI_FadeEffect fadeEffect;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitText;

    [SerializeField] private GameObject pauseUI;

    public bool isPaused;

    private void Awake()
    {
        instance = this;
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseButton();
    }

    public void UpdateFruit(int collectedFruits,int totalFruits)
    {
        fruitText.text = collectedFruits.ToString() + "/" + totalFruits.ToString(); 
    }

    public void UpdateTimer(float timer)
    {
        timerText.text = timer.ToString("000");
    }

    public void PauseButton()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void SwitchToMainMenu()
    {
        Time.timeScale = 1;
        fadeEffect.ScreenFade(1, 1.5f, GoToMainMenu);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
