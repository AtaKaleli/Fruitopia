using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelButton : MonoBehaviour
{

    private int levelIndex;
    private string levelName;
    [SerializeField] private TextMeshProUGUI buttonText;


    public void SetupButton(int newLevelIndex)
    {
        levelIndex = newLevelIndex;
        buttonText.text = "Level " + levelIndex;
        levelName = "Level_" + levelIndex;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }




}
