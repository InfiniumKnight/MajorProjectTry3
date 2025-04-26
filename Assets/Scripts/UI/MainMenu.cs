using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnLoadGameClicked()
    {
        DataPersistenceManager.instance.LoadGame();
    }

    public void OnNewGameClicked()
    {
        SceneManager.instance.players.Clear();
        DataPersistenceManager.instance.NewGame();
    }

    public void ShowCredits()
    {
        SceneManager.instance.LoadLevel("Credits");
    }

    public void QuitGame()
    {
        SceneManager.instance.QuitGame();
    }
}
