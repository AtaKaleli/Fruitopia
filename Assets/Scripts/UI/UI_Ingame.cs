using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Ingame : MonoBehaviour
{
    public static UI_Ingame instance;
    public UI_FadeEffect fadeEffect;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitText;

    private void Awake()
    {
        instance = this;
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
    }

    private void Start()
    {
        fruitText.text = "0";
    }

    public void UpdateFruit(int collectedFruits)
    {
        fruitText.text = collectedFruits.ToString(); 
    }

    public void UpdateTimer(float timer)
    {
        timerText.text = timer.ToString("000") + " s";
    }
}
