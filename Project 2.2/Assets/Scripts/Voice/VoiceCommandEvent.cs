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
    private VoiceManager.VoiceInput CommandInUse;
    private bool IsActive = false;

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

    private void OnDisable()
    {
        VoiceManager.Instance?.RemoveVoiceCommand(CommandInUse);
        IsActive = false;
    }

    private void OnEnable()
    {

        if (IsActive)
        {
            return;
        }
        UpdateCommandInUse();
        VoiceManager.Instance.AddVoiceCommand(CommandInUse);
    }

    private void OnDestroy()
    {
        VoiceManager.Instance?.RemoveVoiceCommand(CommandInUse);
    }

    private void UpdateCommandInUse()
    {
        CommandInUse = new VoiceManager.VoiceInput()
        {
            name = voiceCommandName,
            trigger = voiceCommandtrigger,
            action = () => { OnCommandTrigger.Invoke(); }
        };
    }
}
