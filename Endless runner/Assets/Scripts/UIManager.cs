using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private TextMeshProUGUI distanceScore;

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
    /// updates the score UI and the high score UI.
    /// </summary>
    public void UpdateScoreUI()
    {
        scoreText.text = string.Format("Score: {0}", GameManager.instance.score);
        HighScoreText.text = string.Format("HighScore: {0}", GameManager.instance.highScore);
        distanceScore.text = string.Format("Distance: {0}", GameManager.instance.distance);
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
