using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{

    public delegate void EnemyDeathExp2();
    public static event EnemyDeathExp2 EnemyExp2;

    Vector3 dest;
    private void Start()
    {

    }
    void Update()
    {
        
    }

    //Tells the level up system to add enemy2 exp
    private void OnDestroy()
    {
        EnemyExp2.Invoke();
    }

}
