using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    int damage;
    public float speed = 1.0f;
    float timePassed = 0f;

    public void Launch(int Damage)
    {
        damage = Damage;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > 5f)
    {
        Destroy(gameObject);
    }
    }

    void triggerEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            try
            {
                other.gameObject.GetComponent<EnemyBrain>().EnemyHealthHandler(damage);
                Destroy(gameObject);
            }
            catch
            {
                Destroy(gameObject);
            }
        }

        
    }
}
