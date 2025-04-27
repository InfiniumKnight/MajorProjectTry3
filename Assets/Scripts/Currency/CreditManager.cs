using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditManager : MonoBehaviour, IDataPersistence
{
  public static CreditManager Instance { get; private set; }
    //ensure only one credit manager instance accesible globally ^

    // displays the current credit amount to the player.
    //I think alysa is doing UI not sure if this needs to be here ???
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI creditDisplay;

    // store current amount of credits
    private int currentCredits = 0;

    // property to get/set credits with built-in UI update
    public int Credits
    {
        get { return currentCredits; }
        private set
        {
            currentCredits = value;
            UpdateUI();
        }
    }

    private void Awake()
    {
        // singleton pattern implementation
        //easy glabal access
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() //ensure UI displays the correct credit amount on game start
    {
        UpdateUI();
    }

    // implement 'LoadData' from 'IDataPersistence'
    public void LoadData(GameData data) //load data from saved data
    {
        this.Credits = data.credits;
        Debug.Log($"Loaded {Credits} credits from save data");
    }

    // implement SaveData from IDataPersistence
    public void SaveData(ref GameData data) //saves current crredit amount to data object
    {
        data.credits = this.Credits;
        Debug.Log($"Saved {Credits} credits to game data");
    }

    // add credits (called when enemies are killed)
    public void AddCredits(int amount)
    {
        if (amount < 0)
        { //reject negative inputs
            Debug.LogWarning("Attempted to add negative credits");
            return;
        }

        Credits += amount;
        Debug.Log($"Added {amount} credits. New total: {Credits}");
        //auto update

        // save game after credits change (optional: could impact performance if called too often o_o)
        if (DataPersistenceManager.instance != null)
        {
            DataPersistenceManager.instance.SaveGame();
        }
    }

    // spend credits (returns true if successful, false if not enough credits)
    public bool SpendCredits(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Attempted to spend negative credits");
            return false;
        }

        if (Credits >= amount)
        {
            Credits -= amount;
            Debug.Log($"Spent {amount} credits. Remaining: {Credits}");

            // save game after spending credits
            if (DataPersistenceManager.instance != null)
            {
                DataPersistenceManager.instance.SaveGame();
            }

            return true;
        }
        else
        {
            Debug.Log($"Not enough credits to spend {amount}. Current total: {Credits}");
            return false;
        }
    }

    // update the UI display
    private void UpdateUI()
    {
        if (creditDisplay != null)
        {
            creditDisplay.text = $"Credits: {Credits}";
        }
    }

    // reset credits
    // added just for testing or game reset???
    public void ResetCredits()
    {
        Credits = 0;

        // save game after resetting credits
        if (DataPersistenceManager.instance != null)
        {
            DataPersistenceManager.instance.SaveGame();
        }

        Debug.Log("Credits have been reset to 0");
    }
}
