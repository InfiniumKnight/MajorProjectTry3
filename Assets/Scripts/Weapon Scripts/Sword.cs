using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField] int SwordLevel;

    public float attackSpeed = 5;
    public float timeSinceAttack = 0;

    public GameObject hitBox;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (Characterselected.TankSelected)
        {
            SwordLevel = 1;
        }

        hitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(SwordLevel >= 1)
        {
            if (timeSinceAttack >= attackSpeed)
            {
                StartCoroutine(Attack());
            }
            else
            {
                timeSinceAttack += Time.deltaTime;
            }
        }
    }

    private IEnumerator Attack()
    {
        hitBox.SetActive(true);
        yield return new WaitForSeconds(1);
        hitBox.SetActive(false);
        timeSinceAttack = 0;
    }

    public void SwordLevelUp()
    {
        SwordLevel += 1;
        attackSpeed -= attackSpeed * .15f;
    }
}
