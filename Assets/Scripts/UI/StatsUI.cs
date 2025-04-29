using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public TMP_Text resultText;
    public TMP_Text timeText;
    public TMP_Text playerLevelText;

    private void Start()
    {
        resultText.text = GameStatsManager.instance.didIWin ? "You win" : "You lose";
        timeText.text = "Time played: " + GameStatsManager.instance.GetGameDuration().ToString("F2") + " seconds";
        playerLevelText.text = "Player Level: " + GameStatsManager.instance.playerLevel;
    }

    public void OnContinueButtonClicked()
    {
        DataPersistenceManager.instance.SaveGame();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.instance.LoadLevel("CharacterSelection");
    }
}
