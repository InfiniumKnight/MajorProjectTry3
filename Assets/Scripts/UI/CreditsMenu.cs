using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public void OnContinueClicked()
    {
        SceneManager.instance.LoadLevel("Main Menu");
    }
}
