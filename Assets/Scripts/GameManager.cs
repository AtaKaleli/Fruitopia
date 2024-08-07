using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private int currentLevelIndex;
    

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

    public void LevelFinished()
    {
        int lastIndex = SceneManager.sceneCountInBuildSettings - 2;
        bool isGameOver = currentLevelIndex == lastIndex;

        if(isGameOver)
            UI_Ingame.instance.fadeEffect.ScreenFade(1, 1.5f, LoadTheEndScene);
        else
            UI_Ingame.instance.fadeEffect.ScreenFade(1, 1.5f, LoadNextLevel);

    }

    private void LoadTheEndScene()
    {
        SceneManager.LoadScene("Credits");
    }

    private void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;
        SceneManager.LoadScene("Level_" + nextLevelIndex);
    }

    

    
}
