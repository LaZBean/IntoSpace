using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CanEditMultipleObjects]
[CustomEditor(typeof(SpriteText))]
public class SpriteTextEditor : Editor
{

    public override void OnInspectorGUI()
    {
        SpriteText myTarget = (SpriteText)target;

        myTarget.font = EditorGUILayout.ObjectField("Font", myTarget.font, typeof(SpriteFont)) as SpriteFont;
        myTarget.text = GUILayout.TextField(myTarget.text);
        myTarget.text_formatting = (SpriteText.TextFormatting)EditorGUILayout.EnumPopup("Text Formatting", myTarget.text_formatting);
        myTarget.aligment = (TextAnchor)EditorGUILayout.EnumPopup("Aligment", myTarget.aligment);
        myTarget.offset = EditorGUILayout.FloatField("Offset", myTarget.offset);
        myTarget.color = EditorGUILayout.ColorField("Color", myTarget.color);

        //base.OnInspectorGUI();
        EditorUtility.SetDirty(myTarget);
    }
}

