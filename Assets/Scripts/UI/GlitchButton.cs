using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlitchButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("Glitch");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("Glitch");
    }
}
