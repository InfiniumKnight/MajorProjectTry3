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

    public float healAmount;

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
        GetComponent<PlayerController>().RestoreHealth(healAmount);
    }
}
