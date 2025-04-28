using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int credits;

    public bool AlienUnlocked;
    public bool TankUnlocked;

    public string selectedCharacter;

    //the values defined in this will be default values for when game loads in

    public GameData()
    {
        this.credits = 0;

        //RobotChar is always unlocked
        this.AlienUnlocked = false;
        this.TankUnlocked = false;
    }
}