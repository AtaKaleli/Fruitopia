using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LevelSelection : MonoBehaviour
{

    [SerializeField] private UI_LevelButton levelButtonPref;
    [SerializeField] private Transform buttonParent;
    [SerializeField] private int offLevelScene = 1;

    private void Awake()
    {
        
        PlayerPrefs.SetInt("Level1Unlocked", 1); // first level is always unlocked at the beginning
    }

    private void Start()
    {
        
        CreateLevelButtons();
    }

    private void CreateLevelButtons()
    {

        int amountOfLevel = SceneManager.sceneCountInBuildSettings - offLevelScene; // -1 represents off level screens

        for (int i = 1; i < amountOfLevel; i++)
        {
            UI_LevelButton newButton = Instantiate(levelButtonPref, buttonParent);
            newButton.SetupButton(i);
            Button btn = newButton.GetComponent<Button>();

             
            bool levelUnlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked", 0) == 1;
            
            if (levelUnlocked)
            {
                btn.interactable = true;
                
            }
            else
            {
                
                btn.interactable = false;
            }
        }
    }

}
