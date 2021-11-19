using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject pauzeScreen;
    [SerializeField] private GameObject settingsScreen;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
    
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
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.audioClips[0], AudioManager.instance.audioSources[0]);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// loads the mainmenu scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadMainMenu()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.audioClips[0], AudioManager.instance.audioSources[0]);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    private IEnumerator ExitGame()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.audioClips[0], AudioManager.instance.audioSources[0]);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
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
