using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnLoadGameClicked()
    {
        DataPersistenceManager.instance.LoadGame();
        SceneManager.instance.LoadLevel("CharacterSelection");
    }

    public void OnNewGameClicked()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.instance.LoadLevel("CharacterSelection");
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
