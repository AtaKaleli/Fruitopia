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

    
    public void UpdateFruit(int collectedFruits,int totalFruits)
    {
        fruitText.text = collectedFruits.ToString() + "/" + totalFruits.ToString(); 
    }

    public void UpdateTimer(float timer)
    {
        timerText.text = timer.ToString("000") + " s";
    }
}
