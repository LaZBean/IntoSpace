using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CanEditMultipleObjects]
[CustomEditor(typeof(SimpleSpriteAnimator))]
public class SimpleSpriteAnimatorEditor : Editor
{
    SimpleSpriteAnimator myTarget;

    public override void OnInspectorGUI()
    {
        myTarget = (SimpleSpriteAnimator)target;


        //base.OnInspectorGUI();


        DrawPropertyArray("_frames");

        GUILayout.Space(16);

        myTarget.clearSpriteOnEnd = EditorGUILayout.Toggle("Clear Sprite On End", myTarget.clearSpriteOnEnd);
        myTarget.lifetime = EditorGUILayout.FloatField("Lifetime", myTarget.lifetime);
        myTarget.timeMultiplier = EditorGUILayout.FloatField("Time Multiplier", myTarget.timeMultiplier);
        myTarget.loop = EditorGUILayout.Toggle("Loop", myTarget.loop);

        GUILayout.Space(16);

        myTarget.mode = (SimpleSpriteAnimator.Mode)EditorGUILayout.EnumPopup("Mode", myTarget.mode);

        GUILayout.Space(16);

        if (myTarget.mode == SimpleSpriteAnimator.Mode.SingleRow)
        {
            DrawSingleRawOptions();
        }
        else if (myTarget.mode == SimpleSpriteAnimator.Mode.Grid)
        {
            DrawGridOptions();
        }


        GUILayout.Space(32);

        //if (Event.current.type != EventType.Layout && Event.current.type != EventType.Repaint) return;

        myTarget.useOffset = EditorGUILayout.BeginToggleGroup("Use Offset", myTarget.useOffset);
        myTarget.offsetTransform = (Transform)EditorGUILayout.ObjectField("Offset Transform", myTarget.offsetTransform, typeof(Transform));
        myTarget.beginPos = EditorGUILayout.Vector3Field("Begin Position", myTarget.beginPos);
        DrawPropertyArray("_offsets");
        EditorGUILayout.EndToggleGroup();


        DrawPropertyArray("presets");

        if (GUI.changed)
            EditorUtility.SetDirty(myTarget);
    }



    void DrawSingleRawOptions()
    {
        GUILayout.Label("[SINGLE ROW]");

        GUILayout.Space(16);
        myTarget.curIndx = EditorGUILayout.IntField("Index", myTarget.curIndx);

        //
        if (myTarget.frames == null) return;


        if (Event.current.type != EventType.Layout && Event.current.type != EventType.Repaint) return;

        //SINGLE PREVIEW
        EditorGUILayout.BeginHorizontal();
        
        float scale = Mathf.Clamp(Mathf.Floor((Screen.width - 20) / spriteSize.x) / (myTarget.frames.Length), 0f, 1f); //Calculate sprite scale
        for (int x = 0; x < myTarget.frames.Length; x++)
        {
            DrawSprite(myTarget.frames[x], x == myTarget.curIndx, scale);
        }
        
        EditorGUILayout.EndHorizontal();
    }



    void DrawGridOptions()
    {
        GUILayout.Label("[GRID]");

        myTarget.gridRowLength = EditorGUILayout.IntField("Row Length", myTarget.gridRowLength);
        myTarget.gridOrientation = (SimpleSpriteAnimator.GridOrienation)EditorGUILayout.EnumPopup("Orientation", myTarget.gridOrientation);

        DrawPropertyArray("rowPattern");

        GUILayout.Space(16);

        myTarget.curIndx = EditorGUILayout.IntField("Index", myTarget.curIndx);
        myTarget.gridY = EditorGUILayout.IntField("Row", myTarget.gridY);

        GUILayout.Space(32);

        //
        if (myTarget.frames == null) return;

        //GRID PREVIEW
        EditorGUILayout.BeginVertical();
        
        float scale = Mathf.Clamp(Mathf.Floor(Screen.width / spriteSize.x) / (myTarget.gridRowLength), 0f, 1f); //Calculate sprite scale

        float yCount = (myTarget.frames!=null) ? (myTarget.frames.Length * 1f / myTarget.gridRowLength) : 0;
        
        for (int y = 0; y < yCount; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < myTarget.gridRowLength; x++)
            {
                int id = x + myTarget.gridRowLength * y;
                //int curID = myTarget.curIndx + myTarget.gridRowLength * myTarget.gridY;
                Sprite s = (id >= myTarget.frames.Length) ? null : myTarget.frames[id];
                DrawSprite(s, id == myTarget.actualID, scale);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }


    Vector2 spriteSize = new Vector2(32,32);

    void DrawSprite(Sprite s, bool highlight = false, float scale = 1f)
    {
        if (Event.current.type != EventType.Layout && Event.current.type != EventType.Repaint) return;

        if (!highlight)
            GUI.color = Color.gray;

        Rect rect = GUILayoutUtility.GetRect(spriteSize.x, spriteSize.y, GUILayout.Width(spriteSize.x * scale), GUILayout.Height(spriteSize.y * scale));
        GUI.Box(rect, "");

        if (s){
            GameUtility.GUIDrawSprite(rect, s);     
        }

        GUI.color = Color.white;
    }



    void DrawPropertyArray(string propertyName)
    {
        SerializedProperty sp = serializedObject.FindProperty(propertyName);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(sp, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }




    


}

