using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteText : MonoBehaviour
{
    [SerializeField] SpriteFont _font;
    [SerializeField] string _text = "";
    [SerializeField] TextFormatting _text_formatting = TextFormatting.Normal;
    [SerializeField] TextAnchor _aligment = TextAnchor.UpperLeft;
    [SerializeField] float _offset;
    [SerializeField] Color _color = Color.white;


    RectTransform panel;
    

    public List<Image> charsImages = new List<Image>();

    public enum TextFormatting
    {
        Normal,
        ToUpper,
        ToLower,
    }

    //=====================================================\\

    public SpriteFont font{
        get { return _font; }
        set{
            if (_font == value) return;
            _font = value;
            Refresh();
        }
    }

    public string text{
        get { return _text; }
        set{
            if (_text == value) return;
            _text = value;
            FormattingText();
        }
    }

    public TextFormatting text_formatting
    {
        get { return _text_formatting; }
        set
        {
            if (_text_formatting == value) return;
            _text_formatting = value;

            FormattingText();
        }
    }

    public float offset{
        get { return _offset; }
        set{
            if (_offset == value) return;
            _offset = value;
            Refresh();
        }
    }

    public TextAnchor aligment{
        get { return _aligment; }
        set{
            if (_aligment == value) return;
            _aligment = value;
            Refresh();
        }
    }

    public Color color{
        get { return _color; }
        set{
            if (_color == value) return;
            _color = value;
            RefreshColor();
        }
    }

    //=====================================================\\
    void Start(){
        Refresh();
    }

    //=====================================================\\
    public void Refresh(){

        Clear();

        if (font == null || text == "") return;

        panel = (RectTransform)transform;
        float w = 0;
        float charW = 0;

        for (int i = 0; i < _text.Length; i++){
            for (int c = 0; c < font.chars.Length; c++){

                if (_text[i] != font.chars[c]) continue;

                GameObject charObj = new GameObject("" + _text[i]);
                Image charImg = charObj.AddComponent<Image>();

                Sprite charSprite = font.charsSprites[c];
                charImg.sprite = charSprite;

                charImg.rectTransform.SetParent(panel);

                w += charImg.sprite.rect.width + offset;

                charW = charImg.sprite.rect.width + offset;

                RefreshPos(charImg.rectTransform);

                charImg.rectTransform.localScale = new Vector3(1, 1, 1);
                charImg.SetNativeSize();

                charImg.gameObject.hideFlags = HideFlags.HideInHierarchy;

                charsImages.Add(charImg);
                break;
            }
        }

        if (charsImages.Count == 0) return; 

        float nextPosX = 0;
        Vector2 anchor = Vector2.zero;

        if (aligment == TextAnchor.UpperLeft || aligment == TextAnchor.MiddleLeft || aligment == TextAnchor.LowerLeft){
            w *= 0f; nextPosX += 0; anchor = new Vector2(0, anchor.y);
        }  
        else if (aligment == TextAnchor.UpperCenter || aligment == TextAnchor.MiddleCenter || aligment == TextAnchor.LowerCenter){
            w *= 0.5f; nextPosX += charW/2 - ((charsImages.Count%2==0) ? 0.5f : 0f); anchor = new Vector2(0.5f, anchor.y);
        }
        else{
            w *= 1f; nextPosX += charW; anchor = new Vector2(1f, anchor.y);
        }

        
        foreach (Image img in charsImages){
            Vector2 pivot = new Vector2(
                img.sprite.rect.width * (img.sprite.pivot.x / img.sprite.rect.width) - (img.sprite.rect.width*(anchor.x)), 
                img.sprite.rect.height * (img.sprite.pivot.y / img.sprite.rect.height) - (img.sprite.rect.height * anchor.y));
            img.rectTransform.anchoredPosition = new Vector2(nextPosX - (w), 0) - pivot; //-img.sprite.rect.width * img.sprite.pivot.x, - img.sprite.rect.height * img.sprite.pivot.y
            nextPosX += img.sprite.rect.width + offset;
        }

        RefreshColor();
    }

    //=====================================================\\

    void FormattingText()
    {
        switch (_text_formatting){
            case TextFormatting.ToUpper: text = text.ToUpper(); break;
            case TextFormatting.ToLower: text = text.ToLower(); break;
            case TextFormatting.Normal: break;
        }

        Refresh();
    }

    void RefreshPos(RectTransform rect)
    {
        Rect anchor = GameUtility.GetTextAnchor(aligment);

        rect.anchorMin = new Vector2(anchor.x, anchor.y);
        rect.anchorMax = new Vector2(anchor.width, anchor.height);

        rect.pivot = GameUtility.GetTextAnchorPivot(aligment);
    }

    public void RefreshColor(){
        foreach (Image img in charsImages){
            img.color = color;
        }
    }


    public void Clear(){

        for (int i = 0; i < charsImages.Count; i++){
            if(charsImages[i] != null)
                DestroyImmediate(charsImages[i].gameObject);
        }
        charsImages.Clear();
    }
}
