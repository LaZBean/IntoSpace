using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(AdvancedButton))]
public class AdvancedButtonEditor : ButtonEditor
{
    bool showEvents = true;

    public override void OnInspectorGUI()
    {
      

        base.OnInspectorGUI();

        AdvancedButton myTarget = (AdvancedButton)target;

        this.serializedObject.Update();

        showEvents = EditorGUILayout.Foldout(showEvents, "[Events]");

        if (showEvents)
        {
            GUI.color = new Color(0.8f, 0.8f, 0.95f);
            
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onEnter"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onExit"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onSelect"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onDeselect"), true);

            GUI.color = Color.white;
        }
        

        this.serializedObject.ApplyModifiedProperties();
    }
}
