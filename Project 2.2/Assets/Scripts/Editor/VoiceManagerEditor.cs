using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoiceManager))]
public class VoiceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var voiceManager = (VoiceManager) target;
        if (voiceManager.debugMode)
        {
            base.OnInspectorGUI();
        }
        else
        {
            var Recognizerfailed = serializedObject.FindProperty("onRecognizerFailed");
            EditorGUILayout.PropertyField(Recognizerfailed, true);
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.LabelField("Voice Actions", EditorStyles.boldLabel);

        foreach (var action in voiceManager.GetAllVoiceCommands())
        {
            EditorGUILayout.BeginHorizontal();
            var newTrigger = AddVoiceInput(action);
            if (!action.trigger.Equals(newTrigger))
                //Debug.Log("trigger different " + newTrigger);
                //voiceManager.Actions.EditTrigger(action, newTrigger);
                voiceManager.EditVoiceCommand(action.name, newTrigger);

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Debug Mode", GUILayout.Width(EditorGUIUtility.labelWidth - 4));
        voiceManager.debugMode = EditorGUILayout.Toggle(voiceManager.debugMode);
        EditorGUILayout.EndHorizontal();
    }

    private void ReadOnlyTextField(string label, string text)
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth - 4));
            EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
        }
        EditorGUILayout.EndHorizontal();
    }

    private string AddVoiceInput(VoiceManager.VoiceInput voiceInput)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(voiceInput.name, GUILayout.Width(EditorGUIUtility.labelWidth - 4));
        EditorGUILayout.LabelField(voiceInput.trigger, EditorStyles.miniLabel);
        EditorGUILayout.EndHorizontal();
        return voiceInput.trigger;
    }
}