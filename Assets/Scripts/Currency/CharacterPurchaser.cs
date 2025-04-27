using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPurchaser : MonoBehaviour, IDataPersistence
{
    [System.Serializable]
    public class Character
    {
        public string characterId;//used for unlocking/saving
        public string characterName;//for logs/ui
        public int price;//character cost
        public GameObject characterPrefab;//object in game
        public Button purchaseButton;//this for ui
        public TextMeshProUGUI priceText;//display price text
        public GameObject lockedOverlay;
        public GameObject unlockedIndicator;
    }

    [SerializeField] private List<Character> availableCharacters = new List<Character>();
    private List<string> unlockedCharacters = new List<string>();

    private void Start()
    {
        SetupCharacterButtons();
    }

    // set up UI elements for each character
    private void SetupCharacterButtons()
    {
        foreach (Character character in availableCharacters)
        {
            // setup price text
            if (character.priceText != null)
            {
                character.priceText.text = $"{character.price} Credits";
            }

            // setup button action
            if (character.purchaseButton != null)
            {
                character.purchaseButton.onClick.RemoveAllListeners();
                character.purchaseButton.onClick.AddListener(() => PurchaseCharacter(character));
            }

            // update UI based on unlock status
            UpdateCharacterUI(character);
        }
    }

    // update character UI elements based on unlock status
    //visuals
    private void UpdateCharacterUI(Character character)
    {
        bool isUnlocked = unlockedCharacters.Contains(character.characterId);

        // handle locked overlay
        if (character.lockedOverlay != null)
        {
            character.lockedOverlay.SetActive(!isUnlocked);
        }

        // handle unlocked indicator
        if (character.unlockedIndicator != null)
        {
            character.unlockedIndicator.SetActive(isUnlocked);
        }

        // handle button interactability
        if (character.purchaseButton != null)
        {
            if (isUnlocked)
            {
                // character already unlocked: button could be used for selection instead
                character.purchaseButton.interactable = true;
            }
            else
            {
                // check if player has enough credits
                character.purchaseButton.interactable =
                    CreditManager.Instance != null &&
                    CreditManager.Instance.Credits >= character.price;
            }
        }
    }

    // attempt to purchase a character
    public void PurchaseCharacter(Character character)
    {
        // check if already unlocked
        if (unlockedCharacters.Contains(character.characterId))
        {
            Debug.Log($"Character {character.characterName} is already unlocked!");
            // implement selection logic for already unlocked characters
            return;
        }

        // try to spend credits, check if enough credits
        if (CreditManager.Instance != null && CreditManager.Instance.SpendCredits(character.price))
        {
            // purchase successful
            UnlockCharacter(character.characterId);
            Debug.Log($"Successfully purchased character: {character.characterName}");

            // ppdate UI
            UpdateCharacterUI(character);

            // save the game to persist unlocked state
            if (DataPersistenceManager.instance != null)
            {
                DataPersistenceManager.instance.SaveGame();
            }
        }
        else
        {
            Debug.Log($"Not enough credits to purchase {character.characterName}");
            // display a message to the player here
        }
    }

    // unlock a character by ID
    //add character id to unlock list
    //finds matching 'character' > update ui

    public void UnlockCharacter(string characterId)
    {
        if (!unlockedCharacters.Contains(characterId))
        {
            unlockedCharacters.Add(characterId);

            // update UI for this character
            Character character = availableCharacters.Find(c => c.characterId == characterId);
            if (character != null)
            {
                UpdateCharacterUI(character);
            }
        }
    }

    // IDataPersistence implementation
    public void LoadData(GameData data)
    {
        this.unlockedCharacters = data.playersUnlocked ?? new List<string>();

        // Update UI for all characters after loading
        foreach (Character character in availableCharacters)
        {
            UpdateCharacterUI(character);
        }

        Debug.Log($"Loaded {unlockedCharacters.Count} unlocked characters");
    }

    public void SaveData(ref GameData data) //saves current unlock list to 'GameData'
    {
        data.playersUnlocked = this.unlockedCharacters;
        Debug.Log($"Saved {unlockedCharacters.Count} unlocked characters");
    }

    // update UI when credits change (can be called from an event so buttons get enabled/disable)
    public void OnCreditsChanged()
    {
        foreach (Character character in availableCharacters)
        {
            UpdateCharacterUI(character);
        }
    }
}