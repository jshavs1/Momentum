﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuButton : CustomButton
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        anim.SetBool("Hover", hover);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        anim.SetBool("Hover", hover);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        anim.SetBool("Hover", false);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        anim.SetBool("Hover", true);
    }

    protected override void OnCanvasGroupChanged()
    {
        interactable = GetComponentInParent<CanvasGroup>()?.interactable ?? true; 
    }
}
