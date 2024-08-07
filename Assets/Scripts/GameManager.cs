using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private int currentLevelIndex;
    private int nextLevelIndex;
    public int lastContinueLevelIndex;

    //Ingame UI and Fruit
    private UI_Ingame inGameUI;
    private float levelTimer;
    public int fruitsCollected;
    public int totalFruits;
    public Fruit[] allFruits;
    public Box_Carbon[] allCarbonBoxes;
    public Box_Steel[] allSteelBoxes;


    [Header("Player")]
    public Player player;
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Transform respawnPoint;
    



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
        inGameUI = UI_Ingame.instance;
        inGameUI.fadeEffect.ScreenFade(0, 1f);
    }

    private void Start()
    {

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelIndex = currentLevelIndex + 1;
        lastContinueLevelIndex = PlayerPrefs.GetInt("LastContinueLevelIndex");


        if (currentLevelIndex > lastContinueLevelIndex)
            PlayerPrefs.SetInt("LastContinueLevelIndex", currentLevelIndex); // this is the index of the last level that player played
        
        CollectFruitsInfo();
    }

    private void CollectFruitsInfo()
    {
        allFruits = FindObjectsOfType<Fruit>();
        allCarbonBoxes = FindObjectsOfType<Box_Carbon>();
        allSteelBoxes = FindObjectsOfType<Box_Steel>();
        totalFruits = allFruits.Length + (allCarbonBoxes.Length * 10) + (allSteelBoxes.Length * 10);

        inGameUI.UpdateFruit(0, totalFruits);
        PlayerPrefs.SetInt("Level" + currentLevelIndex + "TotalFruits", totalFruits);
    }

    private void Update()
    {
        levelTimer += Time.deltaTime;
        inGameUI.UpdateTimer(levelTimer);
    }

    public void AddFruit()
    {
        fruitsCollected++;
        inGameUI.UpdateFruit(fruitsCollected,totalFruits);
    }

    public void UpdateRespawnPoint(Transform updatedRespawnPoint)
    {
        respawnPoint = updatedRespawnPoint;
    }

    public void RespawnPlayer(float respawnWaitTime)
    {
        DifficultyManager difficultyManager = DifficultyManager.instance;
        if(difficultyManager != null && difficultyManager.difficulty == DifficultyType.Hard)
        {
            return;
        }
        StartCoroutine(WaitForRespawn(respawnWaitTime));
    }

    IEnumerator WaitForRespawn(float respawnWaitTime)
    {
        yield return new WaitForSeconds(respawnWaitTime);
        GameObject newPlayer = Instantiate(playerPref, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
        player.SetCanMove(false);
    }

    public void RestartLevel()
    {
        UI_Ingame.instance.fadeEffect.ScreenFade(1, .75f, LoadCurrentScene);
    }
  
    private void LoadCurrentScene()
    {
        SceneManager.LoadScene("Level_" + currentLevelIndex);
    }
    private void LoadTheEndScene()
    {
        SceneManager.LoadScene("Credits");
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene("Level_" + nextLevelIndex);
    }

    public void LoadNextScene()
    {

        SaveBestTime();
        SaveFruitInfo();

        int lastIndex = SceneManager.sceneCountInBuildSettings - 2;
        bool isGameOver = currentLevelIndex == lastIndex;

        if (isGameOver)
            inGameUI.fadeEffect.ScreenFade(1, 1.5f, LoadTheEndScene);
        else
        {
            PlayerPrefs.SetInt("Level" + nextLevelIndex + "Unlocked", 1); // If the game is not end, this means we unlocked the next level
            inGameUI.fadeEffect.ScreenFade(1, 1.5f, LoadNextLevel);
        }
    }

    private void SaveBestTime()
    {   
        float lastTime = PlayerPrefs.GetFloat("level" + currentLevelIndex + "BestTime", 999);

        if(levelTimer < lastTime)
            PlayerPrefs.SetFloat("level" + currentLevelIndex + "BestTime", levelTimer);
    }

    private void SaveFruitInfo()
    {
        
        
        int fruitsCollectedBefore = PlayerPrefs.GetInt("Level" + currentLevelIndex + "FruitsCollected");
        if(fruitsCollectedBefore < fruitsCollected)
        {
            PlayerPrefs.SetInt("Level" + currentLevelIndex + "FruitsCollected", fruitsCollected);
            
        }

        int totalFruitsInBank = PlayerPrefs.GetInt("TotalFruitsAmount");
        PlayerPrefs.SetInt("TotalFruitsAmount", totalFruitsInBank + fruitsCollected);

    }

    public void RemoveFruit()
    {
        fruitsCollected--;
        inGameUI.UpdateFruit(fruitsCollected, totalFruits);
    }

    public int GetFruitsCollected()
    {
        return fruitsCollected;
    }
}
