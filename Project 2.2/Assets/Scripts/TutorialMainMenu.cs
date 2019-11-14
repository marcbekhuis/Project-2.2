using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialMainMenu : MonoBehaviour
{
    public List<VoiceCommandEvent> VoiceCommandEvents = new List<VoiceCommandEvent>();
    
    public List<UnityEvent> TutorialEvents = new List<UnityEvent>();

    private int currentTutorial = -1;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var voiceCommandEvent in VoiceCommandEvents)
        {
            voiceCommandEvent.Disable();
        }

        if (SaveManager.Instance.SaveData.EnableTutorial)
            NextTutorial();
    }

    public void NextTutorial()
    {
        if (!IsTutorialFinished())
        {
            TutorialEvents[++currentTutorial]?.Invoke();
        }
        else
        {
            currentTutorial = -1;
        }
    }

    public bool IsTutorialFinished()
    {
        return TutorialEvents.Count == currentTutorial;
    }

    public void OnDisable()
    {
        foreach (var voiceCommandEvent in VoiceCommandEvents)
        {
            voiceCommandEvent.Enable();
        }
    }
}
