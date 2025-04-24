using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    private NavMeshAgent agent;

    public delegate void EnemyEventHandler();
    public static event EnemyEventHandler EnemyDeathEvent; //Trying to make enemy death an event for drops

    //Sets up the enemy to find the player and be able to damage them
    private GameObject player = null;
    private PlayerController playerController = null; 

    public int enemyDamage = 10; //Setting up enemy damage
    public int enemyMaxHealth = 30;
    private int enemyHealth;

    [SerializeField]
    private GameObject coin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        enemyHealth = enemyMaxHealth;
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position); //Updates the players position every frame, allows for tracking
    }

    public void EnemyHealthHandler(int damageTaken)
    {
        enemyHealth -= damageTaken;

        if (enemyHealth <= 0)
        {
            EnemyDeathEvent?.Invoke();
            Instantiate (coin, transform.position, coin.transform.rotation);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.HandleHealth(enemyDamage);
        }
    }

}