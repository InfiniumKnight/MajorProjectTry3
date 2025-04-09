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
    private int boostMultiplier = 5;

    [SerializeField]
    private int kamikazeDamage = 100;

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

        if (timeUntilBoost == 0)
        {
            
            speedBoost();
        }
        
    }

    private void kamikaze()
    { 
        
    }

    private void speedBoost()
    {

        GetComponent<NavMeshAgent>().speed *= boostMultiplier;
        boostTime -= Time.deltaTime;
        Debug.Log("Boost activated!");

    }


}

