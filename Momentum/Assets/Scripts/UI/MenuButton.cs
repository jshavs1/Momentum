using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuButton : Selectable
{
    public Animator anim;
    public UnityEvent OnClick;
    private bool hover;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        hover = true;
        anim.SetBool("Hover", hover);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        hover = false;
        anim.SetBool("Hover", hover);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        anim.SetBool("Hover", false);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (hover)
        {
            anim.SetBool("Hover", true);
            if (interactable)
                OnClick.Invoke();
        }
    }

    protected override void OnCanvasGroupChanged()
    {
        interactable = GetComponentInParent<CanvasGroup>()?.interactable ?? true; 
    }
}
