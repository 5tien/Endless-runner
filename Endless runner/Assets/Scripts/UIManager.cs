using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI HighScoreText;
    private int score;
    private int highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Adds the given amount of point to the player and updates the high score if necessary.
    /// </summary>
    /// <param name="amount">the amount you want to add to the score</param>
    public void AddScore(int amount)
    {
        score = score + amount;
        if(score >= highScore)
        {
            highScore = score;
        }
        UIManager.instance.UpdateScoreUI();
    }

    /// <summary>
    /// updates the score UI and the high score UI.
    /// </summary>
    public void UpdateScoreUI()
    {
        scoreText.text = string.Format("Score: {0}", score);
        HighScoreText.text = string.Format("HighScore: {0}", highScore);
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
