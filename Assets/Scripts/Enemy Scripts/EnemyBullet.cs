using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player = null;
    private PlayerController playerController = null;

    [SerializeField]
    private int bulletDamage = 50;

    public GameObject explosion;

    // Gets the player without needing to drag it over in the editor
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    
    //If it hits the player, deals damage, if it misses just explodes
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.HandleHealth(bulletDamage);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
        }
        else
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
        }
    }

}

