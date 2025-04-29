using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour, IDataPersistence
{
    public static SceneManager instance;

    public int credits = 0;
    public bool AlienUnlocked;
    public bool TankUnlocked;

    public string selectedCharacter;

    private void OnDisable()
    {
        Debug.Log("SceneManager disabled");

    }
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
            return;
        }

        if (DataPersistenceManager.instance == null)
        {
            DataPersistenceManager.instance.RegisterPersistentObject(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("The game has closed");
        }
    }

    public void NewGame()
    {
        credits = 0;
        SaveGame();
    }

    public void SaveGame()
    {
        DataPersistenceManager.instance.SaveGame();
    }

    public void LoadData(GameData data)
    {
        if (data != null)
        {
            credits = data.credits;
            AlienUnlocked = data.AlienUnlocked;
            TankUnlocked = data.TankUnlocked;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.credits = credits;
        data.AlienUnlocked = AlienUnlocked;
        data.TankUnlocked = TankUnlocked;
    }

    public void LoadLevel(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("The game has closed");
    }

    public void EndGame(bool playerWon)
    {
        GameStatsManager.instance.EndGame(playerWon);
        credits += 1;

        SaveGame(); // Immediately saves current credit and game data
        LoadLevel("StatsMenu"); // Load next scene right away
    }

    private IEnumerator LoadAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadLevel(sceneName);
    }
}
