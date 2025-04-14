using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : MonoBehaviour
{
    [SerializeField]
    private float boostTime = 2f;

    [SerializeField]
    private int timeUntilBoost = 5;

    private GameObject player = null;
    private PlayerController playerController = null;

    private int time = 0;
    private int currentTime = 0;
    private int boostMultiplier = 5;

    [SerializeField]
    private int kamikazeDamage = 50;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        time = Mathf.RoundToInt(Time.time);

        currentTime -= time;

        if (timeUntilBoost == 0)
        {
            timeUntilBoost = 5;
            speedBoost();
        }
        
    }

    private void kamikaze()
    { 
        
    }

    private void speedBoost()
    {

        GetComponent<NavMeshAgent>().speed *= boostMultiplier;
        boostTime -= time;
        Debug.Log("Boost activated!");

    }

}

