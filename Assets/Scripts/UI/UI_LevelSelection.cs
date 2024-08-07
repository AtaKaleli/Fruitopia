using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelSelection : MonoBehaviour
{

    [SerializeField] private UI_LevelButton levelButtonPref;
    [SerializeField] private Transform buttonParent;
    [SerializeField] private int offLevelScene = 2;

    private void Start()
    {
        CreateLevelButtons();
    }

    private void CreateLevelButtons()
    {

        int amountOfLevel = SceneManager.sceneCountInBuildSettings - offLevelScene; // -2 represents off level screens

        for (int i = 0; i < amountOfLevel; i++)
        {
            UI_LevelButton newButton = Instantiate(levelButtonPref, buttonParent);
            newButton.SetupButton(i + 1);
        }
    }

}
