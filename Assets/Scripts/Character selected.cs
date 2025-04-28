using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characterselected : MonoBehaviour
{
    public static bool AlienSelected = false;
    public static bool RobotSelected = false;
    public static bool TankSelected = true;
    public static bool HasSelected = false;

    public void SelectAlien()
    {
        AlienSelected = true;
        HasSelected = true;
    }

    public void SelectRobot()
    {
        RobotSelected = true;
        HasSelected = true;
    }

    public void SelectTank()
    {
        TankSelected = true;
        HasSelected = true;
    }
}