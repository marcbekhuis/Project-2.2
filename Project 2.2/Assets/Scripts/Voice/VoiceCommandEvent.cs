using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoiceCommandEvent : MonoBehaviour
{
    private string _lastName = "";
    private string _lastTrigger = "";
    private VoiceManager.VoiceInput CommandInUse;
    private bool IsActive = false;
    public UnityEvent OnCommandTrigger = new UnityEvent();
    public string voiceCommandName = "";
    public string voiceCommandtrigger = "";

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
        if (!VoiceManager.IsInstanceNull)
        {
            VoiceManager.GetInstanceIfNotNull.RemoveVoiceCommand(CommandInUse);
        }
        
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
        if (!VoiceManager.IsInstanceNull)
        {
            VoiceManager.GetInstanceIfNotNull.RemoveVoiceCommand(CommandInUse);
        }
       
    }

    private void UpdateCommandInUse()
    {
        if (voiceCommandName == SaveManager.Instance.ERRORLEVEL.LevelName)
        {
            var a = SaveManager.Instance.ERRORLEVEL;
            CommandInUse = new VoiceManager.VoiceInput()
            {
                action = null,
                name = a.LevelName,
                trigger = a.LevelName
            };
            return;
        }
        CommandInUse = new VoiceManager.VoiceInput()
        {
            name = voiceCommandName,
            trigger = voiceCommandtrigger,
            action = () => { OnCommandTrigger.Invoke(); }
        };
    }

    public void UpdateCommand()
    {
        try
        {
            VoiceManager.Instance.RemoveVoiceCommand(CommandInUse);
            UpdateCommandInUse();
            if (CommandInUse.name == "ERROR")
            {
                return;
            }
            VoiceManager.Instance.AddVoiceCommand(CommandInUse);
        }
        catch (Exception e)
        {
            throw;
        }

    }
}
