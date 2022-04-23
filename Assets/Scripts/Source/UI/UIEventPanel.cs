using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventPanel : MonoBehaviour
{
    public static UIEventPanel i;

    public RectTransform rect;
    public RectTransform popupRect;

    public RectTransform failRect;
    public Text failText;

    public RectTransform winRect;
    public Text winText;

    public RectTransform briefingRect;
    public Text briefingText;

    float t;
    public Vector2 rectStartPos;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        rectStartPos = popupRect.anchoredPosition;
    }

    private void Update()
    {
        popupRect.anchoredPosition = Vector2.Lerp(popupRect.anchoredPosition, rectStartPos, 0.2f);
    }


    public void EventFail(string msg)
    {
        rect.gameObject.SetActive(true);
        failRect.gameObject.SetActive(true);
        failText.text = "Mission Failed \n"+msg;
        GameManager.i.Pause();
    }

    public void EventBriefing(string msg)
    {
        rect.gameObject.SetActive(true);
        briefingRect.gameObject.SetActive(true);
        briefingText.text = "Round " + msg;
        GameManager.i.Pause();
    }

    public void EventWin(string msg)
    {
        rect.gameObject.SetActive(true);
        winRect.gameObject.SetActive(true);
        winText.text = "Round "+msg+" Cleared \n";
        GameManager.i.Pause();
    }


    public void CloseAllPopups()
    {
        GameUtility.DisableAllChildren(popupRect, true);
        rect.gameObject.SetActive(false);
    }


    public void ReloadScene()
    {
        GameManager.i.ReloadScene();
    }

    public void Continue()
    {
        CloseAllPopups();
        GameManager.i.Continue();
    }

    public void NewStage()
    {
        CloseAllPopups();
        SceneManager.i.NewStage();
    }
}
