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
    public List<string> players = new List<string>();

    private void OnDisable()
    {
        Debug.Log("");

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
            //Destroy(gameObject);
        }
    }
    public void LoadNextScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("The game has closed");
        }
    }

    public void LoadData(GameData data)
    {
        players = new List<string>(data.players ?? new List<string>());
    }

    public void SaveData(ref GameData data)
    {
        data.players = new List<string>(players);
    }

    public void CompleteLevel()
    {
        LoadLevel("EndStats"); // After completing the level show what player collected
    }
}
