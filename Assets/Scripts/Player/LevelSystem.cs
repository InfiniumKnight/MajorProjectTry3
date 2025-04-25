using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXP;
    public float requiredXP;

    private float lerpTimer;
    private float delayTimer;

    [Header("UI")]
    public Image frontXPBar;
    public Image backXPBar;

    public GameObject player;

    public float healAmount;


    private void Awake()
    {
        Enemy1.EnemyExp1 += Exp1;
        Enemy2.EnemyExp2 += Exp2;
        Enemy3.EnemyExp3 += Exp3;
    }

    //Unsubscribes the player to the coin counter event when killed or scene changes
    private void OnDestroy()
    {
        Enemy1.EnemyExp1 -= Exp1;
        Enemy2.EnemyExp2 -= Exp2;
        Enemy3.EnemyExp3 -= Exp3;
    }

    private void Start()
    {
        frontXPBar.fillAmount = currentXP / requiredXP;
        backXPBar.fillAmount = currentXP / requiredXP;
    }

    private void Update()
    {
        UpdateXPUI();
        if (currentXP > requiredXP)
            LevelUp();
    }

    public void UpdateXPUI()
    {
        float xpFraction = currentXP / requiredXP;
        float FXP = frontXPBar.fillAmount;
        if (FXP > xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXPBar.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXPBar.fillAmount = Mathf.Lerp(FXP, backXPBar.fillAmount, percentComplete);
            }
        }
    }

    public void GainExperienceFlatRate(float xpGained)
    {
        currentXP += xpGained;
        lerpTimer = 0f;
    }

    public void LevelUp()
    {
        level++;
        frontXPBar.fillAmount = 0f;
        backXPBar.fillAmount = 0f;
        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        player.GetComponent<PlayerController>().RestoreHealth(healAmount);
    }

    private void Exp1()
    {
        currentXP = currentXP + 10;
    }

    private void Exp2()
    {
        currentXP = currentXP + 20;
    }

    private void Exp3()
    {
        currentXP = currentXP + 50;
    }



}
