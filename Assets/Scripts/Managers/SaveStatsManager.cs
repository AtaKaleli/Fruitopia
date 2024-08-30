using UnityEngine;

public class SaveStatsManager : MonoBehaviour
{

    public static SaveStatsManager instance;

    public int fruitsCollectedStat;
    public int enemiesKilledStat;
    public int knockedAmountStat;
    public float playTimeStat;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadFruitsCollected();
        LoadEnemiesKilled();
        LoadKnockedAmount();
        LoadPlayTimeAmount();
    }

    private void Update()
    {
        SavePlayTimeStat();
    }

    private void LoadFruitsCollected()
    {
        fruitsCollectedStat = PlayerPrefs.GetInt("fruitsCollectedStat");
        PlayerPrefs.SetInt("fruitsCollectedStat", fruitsCollectedStat);
    }
    private void LoadEnemiesKilled()
    {
        enemiesKilledStat = PlayerPrefs.GetInt("enemiesKilledStat");
        PlayerPrefs.SetInt("enemiesKilledStat", enemiesKilledStat);
    }
    private void LoadKnockedAmount()
    {
        knockedAmountStat = PlayerPrefs.GetInt("knockedAmountStat");
        PlayerPrefs.SetInt("knockedAmountStat", knockedAmountStat);
    }

    private void LoadPlayTimeAmount()
    {
        playTimeStat = PlayerPrefs.GetFloat("playTimeStat");
        PlayerPrefs.SetFloat("playTimeStat", playTimeStat);
    }


    public void SaveFruitsCollectedStat()
    {
        fruitsCollectedStat++;
        PlayerPrefs.SetInt("fruitsCollectedStat", fruitsCollectedStat);
    }
    public void SaveKnockedAmountStat()
    {
        knockedAmountStat++;
        PlayerPrefs.SetInt("knockedAmountStat", knockedAmountStat);
    }
    public void SaveEnemiesKilledStat()
    {
        enemiesKilledStat++;
        PlayerPrefs.SetInt("enemiesKilledStat", enemiesKilledStat);
    }
    public void SavePlayTimeStat()
    {
        playTimeStat += Time.deltaTime;
        PlayerPrefs.SetFloat("playTimeStat", playTimeStat);
    }





}
