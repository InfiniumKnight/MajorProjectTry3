using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : MonoBehaviour
{
    private bool playerKilled;

    private GameObject player = null;
    private PlayerController playerController = null;

    [SerializeField]
    private int boostMultiplier = 5;

    [SerializeField]
    private int kamikazeDamage = 50;

    [SerializeField]
    private int boostLength = 2;

    private float originalSpeed;

    public GameObject explosion;

    public delegate void EnemyDeathExp3();
    public static event EnemyDeathExp3 EnemyExp3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerKilled = true;
        originalSpeed = GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    { 
            Invoke("SpeedBoost", 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.HandleHealth(kamikazeDamage);
            Destroy(gameObject);
            playerKilled = false;
            Instantiate(explosion, transform.position, explosion.transform.rotation);
        }
    }

    private void SpeedBoost()
    {
        GetComponent<NavMeshAgent>().speed *= boostMultiplier;
        Debug.Log("Boost activated!");
        Invoke("BoostTime", boostLength);
    }

    private void OnDestroy()
    {
        if (playerKilled == true)
        {
            EnemyExp3.Invoke();
        }
    }

    private void BoostTime()
    {
        GetComponent<NavMeshAgent>().speed = originalSpeed;
        Invoke("EnemyMissed", 0);
    }

    private void EnemyMissed()
    {
        Destroy(gameObject);
        playerKilled = false;
        Instantiate(explosion, transform.position, explosion.transform.rotation);
    }

}

