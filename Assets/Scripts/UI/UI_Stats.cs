using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fruitsCollectedText;    
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private TextMeshProUGUI gamePlayedTimeText;
    [SerializeField] private TextMeshProUGUI knockedText;



    private void Start()
    {


        knockedText.text = PlayerPrefs.GetInt("knockedAmountStat").ToString();
        enemiesKilledText.text = PlayerPrefs.GetInt("enemiesKilledStat").ToString();
        gamePlayedTimeText.text = PlayerPrefs.GetFloat("playTimeStat").ToString("0") + " sec";
        fruitsCollectedText.text = PlayerPrefs.GetInt("fruitsCollectedStat").ToString();

    }


}
