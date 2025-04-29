using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    private float startTime;
    private int gameLength;

    private int spawnRateEnemy1 = 0;
    private int spawnRateEnemy2 = 0;
    private int spawnRateEnemy3 = 0;
    private int enemySpawnRate = 0;

    private int[] enemyExperience = { 100, 75, 50 }; //Finish setting up randomization of drops

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject boss;
    private int[] randomSpawnRadius = { -10, -9, -8, -7, -6, -5, 5, 6, 7, 8, 9, 10 };
    private int[] bossSpawnRadius = { -15, -14, -13, -12, -11, -10, 10, 11, 12, 13, 14, 15 };

    [SerializeField] private GameObject player;
    [SerializeField] public Camera playerCam;

    private GameObject spawnedBoss;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Invoke("BossSpawner", 300);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        enemySpawnage();

        spawnRateIncrease();   
    }

    //Spawns in enemies, with it spawning 1 more enemy1 every 10 seconds, enemy2 every 20 and enemy3 every 30
    private void enemySpawnage()
    {
        if (spawnRateEnemy1 > enemySpawnRate)
        {
            for (var i = 0; i < spawnRateEnemy1; i++)
            {
                Instantiate(enemy1, new Vector3(randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)], 0, randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)]), Quaternion.identity);
                if (i == spawnRateEnemy1)
                {
                    i = 0;
                }
            }

            for (var i = 0; i < spawnRateEnemy2; i++)
            {
                Instantiate(enemy2, new Vector3(randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)], 0, randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)]), Quaternion.identity);
                if (i == spawnRateEnemy2)
                {
                    i = 0;
                }
            }

            for (var i = 0; i < spawnRateEnemy3; i++)
            {
                Instantiate(enemy3, new Vector3(randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)], 0, randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)]), Quaternion.identity);
                if (i == spawnRateEnemy3)
                {
                    i = 0;
                }
            }

            enemySpawnRate++;

        }
    }

    private void spawnRateIncrease()
    {
        // Updates the gameLength variable
        totalTime();

        spawnRateEnemy1 = (gameLength / 10) + 1;

        spawnRateEnemy2 = (gameLength / 20);

        spawnRateEnemy3 = (gameLength / 30);

        Debug.Log("1 is " + spawnRateEnemy1 + " 2 is " + spawnRateEnemy2 + " 3 is " + spawnRateEnemy3);
    }

    // Says how long the game has been played
    private void totalTime()
    {
        gameLength = Mathf.RoundToInt(Time.time - startTime);
    }

    private void BossSpawner()
    {
        Vector3 spawnPos = new Vector3(
        bossSpawnRadius[Random.Range(0, bossSpawnRadius.Length)],
        0,
        bossSpawnRadius[Random.Range(0, bossSpawnRadius.Length)]
        );

        GameObject bossInstance = Instantiate(boss, spawnPos, Quaternion.identity);

        EnemyBrain brain = bossInstance.GetComponent<EnemyBrain>();
        if (brain != null)
        {
            brain.isBoss = true; // Mark as boss so SceneManager triggers win when it dies
        }
    }

    /*private void TakeDamage(ClickController clickController) // changed
    {
        health -= 10;

        if (health <= 0)
            Debug.Log("I'm dead now! :(");
    }
    */


}
