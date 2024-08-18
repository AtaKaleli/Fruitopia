using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action OnPlayerRespawn;
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
    public Transform respawnPoint;
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector2 shakeDirection;

    [Header("Managers")]
    [SerializeField] private AudioManager audioManager;


    private void Awake()
    {
        

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (player == null)
            player = FindObjectOfType<Player>();
        if (impulse == null)
            impulse = player.GetComponent<CinemachineImpulseSource>();

    }

    private void Start()
    {
        

        inGameUI = UI_Ingame.instance;
        inGameUI.fadeEffect.ScreenFade(0, 1f);

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelIndex = currentLevelIndex + 1;
        lastContinueLevelIndex = PlayerPrefs.GetInt("LastContinueLevelIndex");

        if (respawnPoint == null)
            respawnPoint = FindObjectOfType<RespawnPoint>().transform;
        


        if (currentLevelIndex > lastContinueLevelIndex)
            PlayerPrefs.SetInt("LastContinueLevelIndex", currentLevelIndex); // this is the index of the last level that player played
        
        
        CollectFruitsInfo();
        CreateManagerIfNeeded();
    }

   

    

    private void Update()
    {
        if (inGameUI == null)
            return;

        levelTimer += Time.deltaTime;
        inGameUI.UpdateTimer(levelTimer);
    }


    private void CreateManagerIfNeeded()
    {
        
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
        AudioManager.instance.PlaySFX(10);
        GameObject newPlayer = Instantiate(playerPref, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
        OnPlayerRespawn?.Invoke();
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

    public void ScreenShake(int facingDirection)
    {
        
        impulse.m_DefaultVelocity = new Vector2(shakeDirection.x * facingDirection, shakeDirection.y);
        impulse.GenerateImpulse();
    }

    private void UpdateCameraImpulseFollow()
    {
        
        impulse = player.GetComponent<CinemachineImpulseSource>();
    }
    private void OnEnable()
    {
        GameManager.OnPlayerRespawn += UpdateCameraImpulseFollow;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerRespawn -= UpdateCameraImpulseFollow;
        
    }

}
