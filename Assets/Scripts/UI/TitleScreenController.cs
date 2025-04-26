using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public CanvasGroup titleScreenGroup;
    public CanvasGroup mainMenuGroup;
    public float transitionTime = 1.0f;

    private bool hasPressedEnter = false;
    private UIFader fader;

    private void Start()
    {
        fader = gameObject.AddComponent<UIFader>();
    }

    private void Update()
    {
        if (!hasPressedEnter && Input.GetKeyDown(KeyCode.Return))
        {
            hasPressedEnter = true;
            StartCoroutine(TransitionToMainMenu());
        }
    }

    private IEnumerator TransitionToMainMenu()
    {
        yield return StartCoroutine(fader.FadeCanvasGroup(titleScreenGroup, 1f, 0f, transitionTime));
        yield return StartCoroutine(fader.FadeCanvasGroup(mainMenuGroup, 0f, 1f, transitionTime));
    }
}
