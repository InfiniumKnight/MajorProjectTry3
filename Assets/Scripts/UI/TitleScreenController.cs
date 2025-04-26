using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public GameObject titleScreenPanel;
    public GameObject mainMenuPanel;

    private bool hasPressedEnter = false;

    private void Update()
    {
        if (!hasPressedEnter && Input.GetKeyDown(KeyCode.Return))
        {
            hasPressedEnter = true;
            titleScreenPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }
}
