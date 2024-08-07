using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private int currentLevelIndex;
    private int nextLevelIndex;
    public int lastContinueLevelIndex;
    

    [Header("Player")]
    public Player player;
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Transform respawnPoint;
    

    public int fruitsCollected;


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

        RespawnPlayer(0f); // respawn the player at the beginning of the level
        UI_Ingame.instance.fadeEffect.ScreenFade(0, 1f);
    }

    private void Start()
    {
        
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelIndex = currentLevelIndex + 1;
        lastContinueLevelIndex = currentLevelIndex; // this is the index of the last level that player played
    }

    public void AddFruit()
    {
        fruitsCollected++;
    }

    public void UpdateRespawnPoint(Transform updatedRespawnPoint)
    {
        respawnPoint = updatedRespawnPoint;
    }

    public void RespawnPlayer(float respawnWaitTime)
    {
        StartCoroutine(WaitForRespawn(respawnWaitTime));
    }

    IEnumerator WaitForRespawn(float respawnWaitTime)
    {
        yield return new WaitForSeconds(respawnWaitTime);
        GameObject newPlayer = Instantiate(playerPref, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
        player.SetCanMove(false);
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
        int lastIndex = SceneManager.sceneCountInBuildSettings - 2;
        bool isGameOver = currentLevelIndex == lastIndex;

        if (isGameOver)
            UI_Ingame.instance.fadeEffect.ScreenFade(1, 1.5f, LoadTheEndScene);
        else
        {
            PlayerPrefs.SetInt("Level" + nextLevelIndex + "Unlocked", 1); // If the game is not end, this means we unlocked the next level
            UI_Ingame.instance.fadeEffect.ScreenFade(1, 1.5f, LoadNextLevel);
        }
    }

    
}
