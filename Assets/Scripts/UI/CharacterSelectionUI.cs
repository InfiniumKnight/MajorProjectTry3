using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    [Header("Robot UI Elements")]
    public Button robotSelectButton;

    [Header("Alien UI Elements")]
    public Button alienSelectButton;
    public Button alienUnlockButton;

    [Header("Tank UI Elements")]
    public Button tankSelectButton;
    public Button tankUnlockButton;

    public TMP_Text creditsText;

    private void Start()
    {
        // Fallback if no character is selected yet
        if (string.IsNullOrEmpty(SceneManager.instance.selectedCharacter))
        {
            SceneManager.instance.selectedCharacter = "RobotChar";
        }

        creditsText.text = $"Credits: {SceneManager.instance.credits}";
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Robot is always unlocked
        robotSelectButton.gameObject.SetActive(true);

        // Alien
        bool alienUnlocked = SceneManager.instance.AlienUnlocked;
        alienSelectButton.gameObject.SetActive(alienUnlocked);
        alienUnlockButton.gameObject.SetActive(!alienUnlocked);

        // Tank
        bool tankUnlocked = SceneManager.instance.TankUnlocked;
        tankSelectButton.gameObject.SetActive(tankUnlocked);
        tankUnlockButton.gameObject.SetActive(!tankUnlocked);

        creditsText.text = $"Credits: {SceneManager.instance.credits}";
    }

    public void UnlockAlien()
    {
        if (SceneManager.instance.credits > 0 && !SceneManager.instance.AlienUnlocked)
        {
            SceneManager.instance.credits--;
            SceneManager.instance.AlienUnlocked = true;
            UpdateUI();
            SceneManager.instance.SaveGame();
            Debug.Log("Alien unlocked");
        }
        else
        {
            Debug.Log("Not enough credits or already unlocked");
        }
    }

    public void UnlockTank()
    {
        if (SceneManager.instance.credits > 0 && !SceneManager.instance.TankUnlocked)
        {
            SceneManager.instance.credits--;
            SceneManager.instance.TankUnlocked = true;
            UpdateUI();
            SceneManager.instance.SaveGame();
            Debug.Log("Tank unlocked");
        }
        else
        {
            Debug.Log("Not enough credits or already unlocked");
        }
    }

    public void SelectCharacter(string characterName)
    {
        SceneManager.instance.selectedCharacter = characterName;
        SceneManager.instance.SaveGame();
        Debug.Log("Selected character: " + characterName);
    }

    public void OnPlayGameClicked()
    {
        if (!string.IsNullOrEmpty(SceneManager.instance.selectedCharacter))
        {
            SceneManager.instance.LoadLevel("GamePlay");
        }
        else
        {
            Debug.LogWarning("No character selected!");
        }
    }
}