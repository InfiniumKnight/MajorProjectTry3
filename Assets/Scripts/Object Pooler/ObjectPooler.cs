using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public int PoolSize = 30;
    private Queue<GameObject> Pool = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject Projectile = Instantiate(ProjectilePrefab);
            Projectile.SetActive(false);
            Pool.Enqueue(Projectile);
        }
    }

    public GameObject GetObject()
    {
        if (Pool.Count > 0)
        {
            GameObject Projectile = Pool.Dequeue();
            Projectile.SetActive(true);
            return Projectile;
        }
        return null;
    }

    public void ReturnObject(GameObject Projectile)
    {
        Projectile.SetActive(false);
        Pool.Enqueue(Projectile);
    }
}
