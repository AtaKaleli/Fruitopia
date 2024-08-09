using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LevelButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI fruitsText;
  

    private int levelIndex;
    private string levelName;
    [SerializeField] private TextMeshProUGUI buttonText;


    public void SetupButton(int newLevelIndex)
    {
        levelIndex = newLevelIndex;
        buttonText.text = "Level " + levelIndex;
        levelName = "Level_" + levelIndex;

        bestTimeText.text = TimerInfoText();
        fruitsText.text = FruitsInfoText();
    }

    public void LoadLevel()
    {
        int difficultyIndex = ((int)DifficultyManager.instance.difficulty);
        PlayerPrefs.SetInt("GameDifficulty", difficultyIndex);
        SceneManager.LoadScene(levelName);
    }

    private string TimerInfoText()
    {
        float timerValue = PlayerPrefs.GetFloat("level" + levelIndex + "BestTime", 999);
        return "Best Time: " + timerValue.ToString("000");
    }

    private string FruitsInfoText()
    {
        int totalFruits = PlayerPrefs.GetInt("Level" + levelIndex + "TotalFruits", 0);

        string totalFruitsText = totalFruits == 0 ? "?" : totalFruits.ToString();

        int fruitsCollected = PlayerPrefs.GetInt("Level" + levelIndex + "FruitsCollected");

        return "Fruits: " + fruitsCollected + "/" + totalFruitsText;
    }

}
