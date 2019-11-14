using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

public class NameRecognizer : MonoBehaviour
{
    public UnityStringEvent onNameChanged = new UnityStringEvent();
    public string prefix = "Your name is : ";
    [SerializeField] private string m_Hypotheses;

    [SerializeField] private string m_Recognitions;

    private DictationRecognizer m_DictationRecognizer;

    void Start()
    {
        m_DictationRecognizer = new DictationRecognizer();

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            m_Recognitions = text + confidence;
            onNameChanged.Invoke(prefix + text);
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            //Debug.LogFormat("Dictation hypothesis: {0}", text);
            m_Hypotheses = text;
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };
    }

    public void StartRecognizer()
    {
        m_DictationRecognizer.Start();
    }

    public void StopRecognizer()
    {
        m_DictationRecognizer.Stop();
    }
    
    private void OnDisable()
    {
        m_DictationRecognizer.Stop();
        m_DictationRecognizer.Dispose();
    }
    
    [Serializable]
    public class UnityStringEvent : UnityEvent<string>
    {
    }
}
