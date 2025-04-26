using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int credits;

    public List<string> players;
    public List<string> playersUnlocked = new List<string>();

    //the values defined in this will be default values for when game loads in

    public GameData()
    {
        this.credits = 0;
        this.players = new List<string>(); //3 players (0-2)
    }
}