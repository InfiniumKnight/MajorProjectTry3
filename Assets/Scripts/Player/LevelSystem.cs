using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public int level = 1;
    public float currentXP = 0;
    public float requiredXP = 10f;

    private float lerpTimer;
    private float delayTimer;

    [Header("UI")]
    public Image frontXPBar;
    public Image backXPBar;
    public GameObject LevelUpScreen;

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
        LevelUpScreen.SetActive(false);
        //frontXPBar.fillAmount = currentXP / requiredXP;
        //backXPBar.fillAmount = currentXP / requiredXP;
        healAmount = player.gameObject.GetComponent<PlayerController>().health;
    }

    private void Update()
    {
        UpdateXPUI();
        if (currentXP >= requiredXP)
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
                percentComplete = Mathf.Min(1, percentComplete);
                frontXPBar.fillAmount = Mathf.Lerp(FXP, backXPBar.fillAmount, percentComplete);
            }
        }

        if (lerpTimer >= 1)
        {
            delayTimer = 0f;
        }
    }

    public void GainExperienceFlatRate(float xpGained)
    {
        currentXP += xpGained;
        lerpTimer = 0f;
    }

    public void LevelUp()
    {
        
        LevelUpScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        level++;
        GameStatsManager.instance.UpdatePlayerLevel(level);

        //frontXPBar.fillAmount = 0f;
        //backXPBar.fillAmount = 0f;
        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        player.GetComponent<PlayerController>().RestoreHealth(healAmount);

        requiredXP += 10f;

    }

    private void Exp1()
    {
        currentXP = currentXP + 5;
    }

    private void Exp2()
    {
        currentXP = currentXP + 10;
    }

    private void Exp3()
    {
        currentXP = currentXP + 50;
        Debug.Log("Enemy 3 exp");
    }



}
