using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILaunchIndicator : MonoBehaviour
{
    public static UILaunchIndicator i;

    [Range(-1f, 1f)]public float deviation = 0;
    [Range(-1f, 1f)]public float mars_deviation = 0;

    
    public Image targetImage;

    public RectTransform indicator_rect;
    public Vector2 indicator_bounds = new Vector2(32, 8);
    public Vector2 indicator_offset = new Vector2(0, 2);
    public float indicator_speed = 0.5f;
    public float indicator_limit = 0.3f;

    public RectTransform mars_rect;
    public Vector2 mars_bounds = new Vector2(32,8);
    public Vector2 mars_offset = new Vector2(0, 4);
    public float mars_speed = 0.5f;
    public float mars_limit = 0.3f;

    public Image highlightImg;
    public Gradient hGradient;
    float highlight_t;
    float hValue;

    void Awake()
    {
        i = this;
    }

    
    void Update()
    {
        if(SceneManager.i.rocket != null && SceneManager.i.rocket.isReady)
        {
            
            
        }
        mars_deviation = Mathf.PingPong(Time.time * mars_speed, mars_limit * 2) - mars_limit;
        deviation = Mathf.PingPong(Time.time * indicator_speed, indicator_limit * 2) - indicator_limit;



        mars_rect.anchoredPosition = new Vector2(Mathf.Sin(mars_deviation * 3.14f) * mars_bounds.x, Mathf.Cos(mars_deviation * 3.14f) * mars_bounds.y) + mars_offset;
        //targetImage.color = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, Mathf.Cos(deviation * 3.14f));
        indicator_rect.anchoredPosition = new Vector2(Mathf.Sin(deviation * 3.14f) * indicator_bounds.x, Mathf.Cos(deviation * 3.14f) * indicator_bounds.y) + indicator_offset;

        if (highlight_t > 0)
        {
            highlightImg.color = Color.Lerp(highlightImg.color, hGradient.Evaluate(hValue), 0.2f);
        }
        else
        {
            highlightImg.color = Color.Lerp(highlightImg.color, Color.white, 0.2f);
        }
        highlight_t -= Time.deltaTime;
    }

    public float GetDeviationValue()
    {
        return Mathf.Abs(mars_deviation - deviation) / (indicator_limit*2f);
    }

    public void Highlight()
    {
        hValue = GetDeviationValue();
        highlight_t = 0.5f;
    }
}
