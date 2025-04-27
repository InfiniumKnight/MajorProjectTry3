using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//i made the credit dropping logic for enemies serprate from, enemy behavior easier to tweak
// this script is to be atteched to enemy prefabs
public class EnemyCreditDropper : MonoBehaviour
{
    [SerializeField] private int creditValue = 5; //default number if not random
    [SerializeField] private bool randomizeCredits = false; //for randmoizer and ranges
    [SerializeField] private int minCredits = 1; //adjustable
    [SerializeField] private int maxCredits = 10;

    // called when enemy dies, then gives player credits
    public void DropCredits()
    {
        int creditsToAdd = creditValue;
        //start with default cred value
        if (randomizeCredits)
        {
            creditsToAdd = Random.Range(minCredits, maxCredits + 1);//retuns random value from set min-max
        }
        //if random, random value credit dropped
        if (CreditManager.Instance != null) //checking availability... no crash >_<!!
        {
            CreditManager.Instance.AddCredits(creditsToAdd); //called to give credit to player
        }
        else
        {
            Debug.LogError("CreditManager instance not found when trying to drop credits");
        } //just in case for bugs o_o!!
    }

    // for enemy death? example i guess???
   //make sure to call 'OnEnemyDeath()' in enemy death method!!!
    public void OnEnemyDeath()
    {
        // enemy dies = credit dropped
        DropCredits();

        // add things like death animation, sound ordestroying enenmy here. 'Destroy(gameObject);'

    }
}