using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public GameObject explosionEffect;

    public float explosionForce = 5f;//hoping to create the effect of enemies getting slightly pushed by the explosion
    public float radius = 1f;//setting it high for testing, can be changed later.

    public  float delay = .5f;
    public int damage = 15;//thinking low damage since its an area attack

    private void Start()
    {
        Invoke("Explode", delay);
    }

    private void Explode()//makes grenade explode
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        try
        {
            foreach (Collider near in colliders)
            {
                if (near.gameObject.CompareTag("Enemy"))
                {
                    Rigidbody rig = near.GetComponent<Rigidbody>();
                    near.gameObject.GetComponent<EnemyBrain>().EnemyHealthHandler(damage);
                }
            }

            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        catch
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        
    }
}
