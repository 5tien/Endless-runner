using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [SerializeField] private UIManager uiManager;

    public int score;
    public int highScore;
    public int distance;

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

    public void SetDistance(int amount)
    {
        if (amount > 0)
            distance = amount;
        else
            distance = 0;

        UIManager.instance.UpdateScoreUI();
    }


    public void Death()
    {
        
    }
}
