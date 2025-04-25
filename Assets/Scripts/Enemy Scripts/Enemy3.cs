using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : MonoBehaviour
{
    [SerializeField]
    private float boostTime = 2f;

    [SerializeField]
    private int timeUntilBoost = 5;

    private bool playerKilled;

    private GameObject player = null;
    private PlayerController playerController = null;

    private int time = 0;
    private int currentTime = 0;
    private int boostMultiplier = 5;

    [SerializeField]
    private int kamikazeDamage = 50;

    public ParticleSystem explosion;

    public delegate void EnemyDeathExp3();
    public static event EnemyDeathExp3 EnemyExp3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerKilled = true;
    }

    // Update is called once per frame
    void Update()
    {
        time = Mathf.RoundToInt(Time.time);

        currentTime -= time;

        if (timeUntilBoost <= 0)
        {
            timeUntilBoost = 5;
            speedBoost();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.HandleHealth(kamikazeDamage);
            Destroy(gameObject);
            playerKilled = false;
        }
    }



    private void speedBoost()
    {

        GetComponent<NavMeshAgent>().speed *= boostMultiplier;
        boostTime -= time;
        Debug.Log("Boost activated!");

    }

    private void OnDestroy()
    {
        if (playerKilled == true)
        {
            EnemyExp3.Invoke();
        }
    }
}

