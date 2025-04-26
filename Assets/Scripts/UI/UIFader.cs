using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFader : MonoBehaviour
{
    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;

        cg.alpha = startAlpha;
        cg.interactable = false;
        cg.blocksRaycasts = false;

        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            yield return null;
        }

        cg.alpha = endAlpha;
        cg.interactable = endAlpha == 1;
        cg.blocksRaycasts = endAlpha == 1;
    }
}
