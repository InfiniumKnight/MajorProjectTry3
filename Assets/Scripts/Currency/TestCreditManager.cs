// TestCreditManager.cs
using UnityEngine;

public class TestCreditManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CreditManager.Instance.AddCredits(10);
            Debug.Log("Added 10 credits");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            bool success = CreditManager.Instance.SpendCredits(5);
            Debug.Log("Tried to spend 5 credits. Success: " + success);
        }
 
        if (Input.GetKeyDown(KeyCode.P))
        {
            DataPersistenceManager.instance.SaveGame();
            Debug.Log("Manually saved game");
        }
    }
}