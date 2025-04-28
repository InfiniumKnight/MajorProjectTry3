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
            selectedCharacter = data.selectedCharacter;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.credits = credits;
        data.AlienUnlocked = AlienUnlocked;
        data.TankUnlocked = TankUnlocked;
        data.selectedCharacter = selectedCharacter;
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

    public void CompleteLevel()
    {
        LoadLevel("EndStats"); // After completing the level show what player collected
    }
}
