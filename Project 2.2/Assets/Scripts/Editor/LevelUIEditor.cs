using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(LevelUI))]
public class LevelUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelUI levelUi = (LevelUI) target;

        if (GUILayout.Button("update"))
        {
            levelUi.UpdateValues();
        }
    }
}
