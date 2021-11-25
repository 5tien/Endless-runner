using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

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

        DontDestroyOnLoad(this.gameObject);
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
        }
        UIManager.instance.UpdateScoreUI();
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
