using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager i;

    public SpriteText money_text;
    public SpriteText mancount_text;

    public UIPanelButton middle_button;
    public UIPanelButton left_button;
    public UIPanelButton right_button;

    void Awake()
    {
        i = this;
    }

    
    void Update()
    {
        if(PlayerManager.my != null)
        {
            money_text.text = GetNumber(PlayerManager.my.money) + " ";
            mancount_text.text = GetNumber(PlayerManager.my.man_count);

            
        }
    }

    string GetNumber(int n)
    {
        if (n != 0)
            return (n).ToString("#,#", CultureInfo.InvariantCulture);
        else return 0 + "";
    }
}
