using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] int PistolLevel;

    //weapon characteristics
    public float attackSpeed = 3;
    public float timeSinceAttack = 0;
    public int damage = 20;

    public Transform pistol;

    [SerializeField] private Transform Target;

    public GameObject projectilePrefab;
    public GameObject player;
    public GameObject LevelUpScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (Characterselected.AlienSelected)
        {
            PistolLevel = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (PistolLevel >= 1)
        {
            if (Target == null || Vector3.Distance(Target.transform.position, player.transform.position) > 8f)
            {
                FindTarget();
            }
            else
            {
                pistol.transform.LookAt(Target);
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
    }

    private void Attack()
    {
        Rigidbody rb = Instantiate(projectilePrefab, pistol.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(pistol.transform.forward * 10f, ForceMode.Impulse);
        Projectile projectile = rb.GetComponent<Projectile>();
        projectile.Launch(damage);
        timeSinceAttack = 0;
    }

    public void PistolLevelUp()
    {
        PistolLevel += 1;
        attackSpeed -= attackSpeed * .15f;
        LevelUpScreen.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void FindTarget()
    {
        try
        {
            Target = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
        }
        catch
        {
            return;
        }
    }
}
