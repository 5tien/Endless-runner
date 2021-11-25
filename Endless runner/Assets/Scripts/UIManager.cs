using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Score Text")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Menu Objects")]
    [SerializeField] private GameObject pauzeScreen;
    [SerializeField] private GameObject mainMenuScreen;

    [Header("Settings Screen")]
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private Slider volumeSlider;

    [Header("Death Screen Components")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI scoreDeathText;
    [SerializeField] private TextMeshProUGUI highDeathText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume") == true)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            VolumeSlider(PlayerPrefs.GetFloat("Volume"));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauzeScreen();
        }
    }

    public void VolumeSlider(float value)
    {
        AudioManager.instance.SetVolumeLevel(value);
        PlayerPrefs.SetFloat("Volume", value);
    }

    /// <summary>
    /// updates the score UI and the high score UI.
    /// </summary>
    public void UpdateScoreUI()
    {
        if (scoreText != null || highScoreText != null)
        {
            scoreText.text = string.Format("Score: {0}", GameManager.instance.score);
            highScoreText.text = string.Format("HighScore: {0}", GameManager.instance.highScore);
        }
        else
        {
            Debug.LogError("ScoreText and/or HighScoreText are/is empty");
        }
    }

    /// <summary>
    /// loads the game scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadGame()
    {
        AudioManager.instance.PlaySoundEffect(0);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
        AudioManager.instance.PlayBackGroundMusic(2);
    }

    /// <summary>
    /// loads the mainmenu scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadMainMenu()
    {
        AudioManager.instance.PlaySoundEffect(0);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.PlayBackGroundMusic(1);
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    private IEnumerator ExitGame()
    {
        AudioManager.instance.PlaySoundEffect(0);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    /// <summary>
    /// this will reset the highscore and also reset the saved highscore in player prefs
    /// </summary>
    public void ResetSavedData()
    {
        AudioManager.instance.PlaySoundEffect(0);
        PlayerPrefs.DeleteAll();
        volumeSlider.value = 1;
        AudioManager.instance.SetVolumeLevel(1);
        GameManager.instance.score = 0;
        GameManager.instance.highScore = 0;
        UpdateScoreUI();
    }

    /// <summary>
    /// sets the DeathScreen gameobject on 
    /// </summary>
    public void DeathScreeen()
    {
        if(deathScreen != null)
        {
            deathScreen.SetActive(true);
            scoreDeathText.text = string.Format("Your Score: {0}", GameManager.instance.score);
            highDeathText.text = string.Format("Your HighScore: {0}", GameManager.instance.highScore);
        }
    }

    /// <summary>
    /// if the PauzeScreen is active it will be turned of and if PauzeScreen is inactive it wil be turned on;
    /// </summary>
    public void PauzeScreen()
    {
        if (pauzeScreen != null)
        {
            if (pauzeScreen.active == true)
            {
                pauzeScreen.SetActive(false);
                Time.timeScale = 1;
                AudioManager.instance.PlaySoundEffect(0);
            }
            else if (pauzeScreen.active == false)
            {
                pauzeScreen.SetActive(true);
                settingsScreen.SetActive(false);
                Time.timeScale = 0;
                AudioManager.instance.PlaySoundEffect(0);
            }
        }
    }

    /// <summary>
    /// if the SettingsScreen is active it will be turned of and if SettingsScreen is inactive it wil be turned on;
    /// </summary>
    public void SettingsScreen()
    {
        if (mainMenuScreen != null)
        {
            if (settingsScreen.active == true)
            {
                settingsScreen.SetActive(false);
                mainMenuScreen.SetActive(true);
                AudioManager.instance.PlaySoundEffect(0);
            }
            else if (settingsScreen.active == false)
            {
                settingsScreen.SetActive(true);
                mainMenuScreen.SetActive(false);
                AudioManager.instance.PlaySoundEffect(0);
            }
        }else if(pauzeScreen != null)
        {
            if (settingsScreen.active == true)
            {
                settingsScreen.SetActive(false);
                pauzeScreen.SetActive(true);
                AudioManager.instance.PlaySoundEffect(0);
            }
            else if(settingsScreen.active == false)
            {
                settingsScreen.SetActive(true);
                pauzeScreen.SetActive(false);
                AudioManager.instance.PlaySoundEffect(0);
            }
            
        }
    }

    public void ExitGameButton()
    {
        StartCoroutine("ExitGame");
    }
    public void LoadMainMenuButton()
    {
        StartCoroutine("LoadMainMenu");
    }
    public void LoadGameButton()
    {
        StartCoroutine("LoadGame");
    }
}
