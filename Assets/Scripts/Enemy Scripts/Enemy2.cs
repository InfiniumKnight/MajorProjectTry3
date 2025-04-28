using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{

    public delegate void EnemyDeathExp2();
    public static event EnemyDeathExp2 EnemyExp2;

    public GameObject spawner;

    public NavMeshAgent enemy2;

    public Transform player;

    public Camera playerCam;

    public float enemy2Speed;

    Vector3 dest;
    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Ground");
        playerCam = spawner.GetComponent<EnemySpawning>().playerCam;
    }
    void Update()
    {
        //Calculate the player's Camera's frustum planes
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);

        //Get the enemy2s distance from the player
        float distance = Vector3.Distance(transform.position, player.position);

        //If enemy2 is in the player's Camera's view,
        if (GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            enemy2.speed = 0; //enemy2's speed will equal to 0
            enemy2.SetDestination(transform.position); //enemy2's destination will be set to themselves to stop a delay in the movement stopping
        }

        //If enemy2 isn't in the player's Camera's view,
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            enemy2.speed = enemy2Speed; //enemy2's speed will equal to the value of enemy2Speed
            dest = player.position; //dest will equal to the player's position
            enemy2.destination = dest; //enemy2's destination will equal to dest
        }
    }

    //Tells the level up system to add enemy2 exp
    private void OnDestroy()
    {
        EnemyExp2.Invoke();
    }

}
