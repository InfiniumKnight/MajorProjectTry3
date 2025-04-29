using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characterselected : MonoBehaviour
{
    public static bool AlienSelected = false;
    public static bool RobotSelected = false;
    public static bool TankSelected = false;
    public static bool HasSelected = false;

    public static void SelectAlien()
    {
        AlienSelected = true;
        HasSelected = true;
    }

    public static void SelectRobot()
    {
        RobotSelected = true;
        HasSelected = true;
    }

    public static void SelectTank()
    {
        TankSelected = true;
        HasSelected = true;
    }
}