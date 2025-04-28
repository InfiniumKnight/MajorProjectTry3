using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    //weapon characteristics
    public float attackSpeed = 3;
    public float timeSinceAttack = 0;
    public int damage = 20;

    public Transform bulletSpawn;

    [SerializeField] private Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Target == null)
            {
                FindTarget();
            }
            else
            {
                bulletSpawn.transform.LookAt(Target);
            }

            if (timeSinceAttack >= attackSpeed)
            {
                Attack();
            }
            else
            {
                timeSinceAttack += Time.deltaTime;
            }
    }

    private void Attack()
    {
        Rigidbody rb = Instantiate(bullet, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawn.transform.forward * 30f, ForceMode.Impulse);
        Projectile projectile = rb.GetComponent<Projectile>();
        timeSinceAttack = 0;
    }

    public void FindTarget()
    {
        try
        {
            Target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        catch
        {
            return;
        }
    }
}
