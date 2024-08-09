using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Difficulty : MonoBehaviour
{
    private DifficultyManager difficultyManager;

    private void Start()
    {
        difficultyManager = DifficultyManager.instance;
    }


    public void SetEasyMode()
    {
        difficultyManager.SetDifficuilty(DifficultyType.Easy);
    }
    public void SetNormalMode()
    {
        difficultyManager.SetDifficuilty(DifficultyType.Normal);
    }
    public void SetHardMode()
    {
        difficultyManager.SetDifficuilty(DifficultyType.Hard);
    }
}
