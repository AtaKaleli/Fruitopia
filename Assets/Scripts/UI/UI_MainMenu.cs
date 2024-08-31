using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    private UI_FadeEffect fadeEffect;
    [SerializeField] private GameObject[] UI_Elements;
    [SerializeField] private GameObject continueButton;

    [SerializeField] private UI_Settings[] volumeController;
    private bool firstPlay;

    private void Awake()
    {
        SetEnableContinueButton();
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();



        
        Application.targetFrameRate = 60; // for optimization (spped ups game)
        QualitySettings.vSyncCount = 0;
        //Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow, 60); // for optimization*/

    }
    private void Start()
    {
        for (int i = 0; i < volumeController.Length; i++)
        {
            volumeController[i].GetComponent<UI_Settings>().SetupVolumeSlider();
        }

        fadeEffect.ScreenFade(0, 1.5f);
    }

    public void SwitchToNewGame()
    {

        fadeEffect.ScreenFade(1, 1.5f, NewGame);

    }

    private void NewGame()
    {
        SceneManager.LoadScene("Level_1");

    }

    public void SwitchToCredits()
    {
        AudioManager.instance.PlaySFX(4, false);
        fadeEffect.ScreenFade(1, 1.5f, LoadTheCreditsScene);
    }




    private void LoadTheCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void SwicthToUI(GameObject UI_Element)
    {
        foreach (GameObject ui in UI_Elements)
        {
            ui.SetActive(false);
        }
        UI_Element.SetActive(true);
        AudioManager.instance.PlaySFX(4, false);
    }

    private void SetEnableContinueButton()
    {
        bool firstLevelPassed = PlayerPrefs.GetInt("Level2Unlocked", 0) == 1; // if player passed the first level, from now on, player can continue to the level.
        if (firstLevelPassed)
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);
    }

    public void SwitchToContinueLevel()
    {

        fadeEffect.ScreenFade(1, 1.5f, LoadContinueLevel);

    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void LoadContinueLevel()
    {
        int difficultyIndex = PlayerPrefs.GetInt("GameDifficulty", 1);
        int lastContinueLevelIndex = PlayerPrefs.GetInt("LastContinueLevelIndex");

        DifficultyManager.instance.LoadDifficulty(difficultyIndex);
        SceneManager.LoadScene("Level_" + lastContinueLevelIndex);
    }



}
