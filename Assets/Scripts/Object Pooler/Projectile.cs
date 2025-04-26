using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public float LifeTime = 2f;
    private float Timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        Timer = LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            ReturnToPool();
        }
    }
    void ReturnToPool()
    {
        FindObjectOfType<ObjectPooler>().ReturnObject(gameObject);
    }
}
