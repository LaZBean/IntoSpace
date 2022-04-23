using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIPanelButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onPress;

    public Vector2 start_pos;
    public Vector2 press_pos;

    public RectTransform rect;

    
    public bool isPressed = false;
    public float pressSpeed = 2f;

    public float t;
    public float press_t;
    public float press_time = 0.3f;

    void Start()
    {
        start_pos = rect.anchoredPosition;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Press();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UnPress();
    }


    public void Press()
    {
        if (!isPressed)
        {
            isPressed = true;
            onPress.Invoke();
            press_t = press_time;
        }

        isPressed = true;
    }

    public void UnPress()
    {
        isPressed = false;
    }

    
    void Update()
    {
        if (isPressed || press_t>0)
            t = Mathf.Clamp01(t + Time.deltaTime * pressSpeed);
        else
            t = Mathf.Clamp01(t - Time.deltaTime * pressSpeed);

        if(press_t > 0)
            press_t -= Time.deltaTime;

        rect.anchoredPosition = Vector2.Lerp(start_pos, press_pos, t);
    }
}
