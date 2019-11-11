using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoiceCommandEvent : MonoBehaviour
{
    public string voiceCommandName = "";
    public string voiceCommandtrigger = "";
    public UnityEvent OnCommandTrigger = new UnityEvent();

    private string _lastName = "";
    private string _lastTrigger = "";
    
    private void Start()
    {
        VoiceManager.Instance.AddVoiceCommand(voiceCommandName, voiceCommandtrigger, () => {OnCommandTrigger.Invoke(); });
    }

    private void OnValidate()
    {
        //only work if last name != current name
        //only work if last trigger == last name
        //only work if  
        if (_lastName == voiceCommandName)
        {
            return;
        }        
        
        if (_lastTrigger == _lastName && _lastTrigger == voiceCommandtrigger )
        {
            voiceCommandtrigger = voiceCommandName;
        }

        _lastName = voiceCommandName;
        _lastTrigger = voiceCommandtrigger;
    }
}
