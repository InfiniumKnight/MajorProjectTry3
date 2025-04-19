using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int credits;
    public int playersUnlocked;

    //the values defined in this will be default values for when game loads in

    public GameData()
    {
        this.credits = 0;
        this.playersUnlocked = 0;
    }
}