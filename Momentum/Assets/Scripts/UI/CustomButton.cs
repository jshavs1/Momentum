using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class CustomButton : Selectable
{
    public Animator anim;
    public UnityEvent OnClick;
    private bool _hover;
    protected bool hover
    {
        get { return _hover; }
        set
        {
            _hover = value;
            if (value)
                OnHoverEnter();
            else
                OnHoverExit();
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        hover = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        hover = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (!hover) { return; }
        if (interactable)
            OnClick.Invoke();
    }

    public virtual void OnHoverEnter() { }

    public virtual void OnHoverExit() { }

    protected override void OnCanvasGroupChanged()
    {
        interactable = GetComponentInParent<CanvasGroup>()?.interactable ?? true;
    }
}
