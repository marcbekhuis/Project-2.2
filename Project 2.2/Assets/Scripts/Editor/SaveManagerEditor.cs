using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;

[CustomEditor(typeof(SaveManager))]
[CanEditMultipleObjects]
public class SaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        SaveManager saveManager = (SaveManager) target;

        if (Application.isPlaying)
        {
            if (GUILayout.Button("save"))
            {
                saveManager.SaveSettings();
            }

            if (GUILayout.Button("Load"))
            {
                saveManager.LoadSettings();
            }
            if (GUILayout.Button("show Json"))
            {
                Process.Start("notepad.exe", saveManager.SaveFilePath);
                Debug.Log(saveManager.SaveFilePath);
            }
        }
        
        base.OnInspectorGUI();
    }
}
