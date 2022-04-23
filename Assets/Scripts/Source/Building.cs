using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int level;
    public Sprite[] sprites;
    public int[] level_costs;

    public SpriteRenderer renderer;

    public SpriteText ui_lvl;
    public RectTransform ui_button;

    void Start()
    {
        
    }

    
    public void Upgrade()
    {
        PlayerManager.my.money -= level_costs[level+1];
        level++;
        renderer.sprite = (sprites[level]);
    }

    public void Update()
    {
        if ((level+1) <= level_costs.Length-1 && level_costs[level + 1] <= PlayerManager.my.money)
        {
            ui_button.gameObject.SetActive(true);
        }
        else
        {
            ui_button.gameObject.SetActive(false);
        }

        ui_lvl.text = level+1 + " ";
    }
}
