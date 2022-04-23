using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRocketInfo : MonoBehaviour
{
    

    [Range(0f, 1f)]public float rocket_fill = 0.5f;

    public Image slider_fill;
    public Image slider_fill2;

    public RectTransform rect;

    public RectTransform text_rect;
    public SpriteText text;

    public Vector2Int[] fill_levels;

    public Vector2 rect_start_pos;
    float rect_t;

    void Start()
    {
        rect_start_pos = rect.anchoredPosition;
    }

    
    void Update()
    {
        if(SceneManager.i.rocket == null || SceneManager.i.rocket.isLaunched)
        {
            rect.gameObject.SetActive(false);
        }
        else
        {
            rect.gameObject.SetActive(true);
            int people = SceneManager.i.rocket.entities_inside.Count;

            text.text = people + " ";
            slider_fill.fillAmount = Mathf.Clamp01(CurLevel(people) * 1f / fill_levels.Length);
            //slider_fill2.fillAmount = people*1f / 40;

            if(CurLevel(people) == fill_levels.Length + 1)
            {
                rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, rect_start_pos, 0.4f);

                if(rect_t <= 0)
                {
                    rect.anchoredPosition = rect_start_pos + new Vector2(Random.Range(-1f,1f), Random.Range(-1f, 1f)) * 5;
                    rect_t = 0.1f;
                }
                rect_t -= Time.deltaTime;

                //text.color = Color.red;
            }
            else
            {
                //text.color = Color.white;
            }
        }
    }

    int CurLevel(int n)
    {
        for (int i = 0; i < fill_levels.Length; i++)
        {
            if(fill_levels[i].x <= n && fill_levels[i].y >= n)
                return i;
        }
        return fill_levels.Length+1;
    }
}
