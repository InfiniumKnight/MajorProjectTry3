using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] int GrenadeLevel;

    //weapon characteristics
    public float attackSpeed = 4;
    public float timeSinceAttack = 0;

    public Transform grenade;

    //object assignments
    public GameObject grenadePrefab;
    public GameObject player;
    public GameObject LevelUpScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (Characterselected.TankSelected)
        {
            GrenadeLevel = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GrenadeLevel >= 1)
        {
            if (timeSinceAttack >= attackSpeed)
            {
                Attack();
            }
            else
            {
                timeSinceAttack += Time.deltaTime;
            }
        }
    }

    private void Attack()
    {
        //finds a random spot around the player to drop the grenade
        Vector3 randomSpawnPostion = new Vector3(grenade.transform.position.x + Random.Range(-5, 6), 5, grenade.transform.position.z +Random.Range(-5,6));
        //spawns grenade
        Instantiate(grenadePrefab, randomSpawnPostion, Quaternion.identity);
        timeSinceAttack = 0;
    }

    public void GrenadeLevelUp()
    {
        GrenadeLevel += 1;
        attackSpeed -= attackSpeed * .15f;
        LevelUpScreen.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
}
