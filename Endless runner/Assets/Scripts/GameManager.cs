using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [SerializeField] private UIManager uiManager;

    public int score;
    public int highScore;
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

        highScore = PlayerPrefs.GetInt("HighScore",0);
        uiManager.UpdateScoreUI();
    }

    private void Start()
    {
        AudioManager.instance.PlayBackGroundMusic(1);
    }

    /// <summary>
    /// Adds the given amount of point to the player and updates the high score if necessary.
    /// </summary>
    /// <param name="amount">the amount you want to add to the score</param>
    public void AddScore(int amount)
    {
        score = score + amount;
        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        uiManager.UpdateScoreUI();
    }
}
