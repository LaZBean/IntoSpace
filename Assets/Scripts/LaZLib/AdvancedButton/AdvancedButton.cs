﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdvancedButton : Button {

    public UnityEvent onEnter;
    public UnityEvent onExit;
    public UnityEvent onSelect;
    public UnityEvent onDeselect;




    void Update() {

        
	}



    public void RectScale(float value = 1f)
    {
        targetGraphic.rectTransform.localScale = Vector3.one * value;
    }





    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        onEnter.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        onExit.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    

    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        onSelect.Invoke();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        onDeselect.Invoke();
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
    }

    public override void Select()
    {
        base.Select();
    }

}
