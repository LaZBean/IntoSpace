using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteFont", menuName = "UI/Sprite Font", order = 1)]
public class SpriteFont : ScriptableObject
{
    public string chars = "";
    public Sprite[] charsSprites = new Sprite[0];
}
