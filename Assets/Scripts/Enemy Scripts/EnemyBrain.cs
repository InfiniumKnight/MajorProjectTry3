using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    private NavMeshAgent agent;

    //Sets up the enemy to find the player and be able to damage them
    private GameObject player = null;
    private PlayerController playerController = null; 

    public int enemyDamage = 10; //Setting up enemy damage



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position); //Updates the players position every frame, allows for tracking
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController == null) // Only checks for the player controller once
            {
                playerController = collision.gameObject.GetComponent<PlayerController>();
            }

            playerController.HandleHealth(enemyDamage); // Takes damage once per hit

        }
    }

}