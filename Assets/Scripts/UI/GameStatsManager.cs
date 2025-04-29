using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    public static GameStatsManager instance;

    public float gameStartTime;
    public float gameEndTime;
    public int playerLevel;
    public bool didIWin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        gameStartTime = Time.time;
        playerLevel = 1;
        didIWin = false;
    }

    public void EndGame(bool playerWon)
    {
        gameEndTime = Time.time;
        didIWin = playerWon;
    }

    public float GetGameDuration()
    {
        return gameEndTime - gameStartTime;
    }

    public void DisplayGameStats()
    {
        float gameDuration = GetGameDuration();
        Debug.Log("Game Duration: " + gameDuration.ToString("F2") + "seconds");

        string result = didIWin ? "You win" : "You lose";
        Debug.Log(result);
    }

    public void UpdatePlayerLevel(int newLevel)
    {
        playerLevel = newLevel;
    }
}
