using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [SerializeField] private UIManager uiManager;

    public bool gameRunning;
    private bool coroutineRunning;

    public float score;
    public float highScore;
    public float distance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        highScore = PlayerPrefs.GetFloat("HighScore",0);
        uiManager.UpdateScoreUI();
    }

    private void Update()
    {
        if(coroutineRunning == false && gameRunning == true)
        {
            StartCoroutine(Timer());
            //StartCoroutine("Timer");
            coroutineRunning = true;
        }
    }

    /// <summary>
    /// adds 10 points every second.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Timer()
    {
        AddScore(10);
        yield return new WaitForSeconds(1f);
        coroutineRunning = false;
    }

    /// <summary>
    /// Adds the given amount of point to the player and updates the high score if necessary.
    /// </summary>
    /// <param name="amount">the amount you want to add to the score</param>
    public void AddScore(float amount)
    {
        score = score + amount;
        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        uiManager.UpdateScoreUI();
    }

    public void SetDistance(int amount)
    {
        if (amount > 0)
            distance = amount;
        else
            distance = 0;

        uiManager.UpdateScoreUI();
    }

    public void Death()
    {
        uiManager.DeathScreen();
    }
}
