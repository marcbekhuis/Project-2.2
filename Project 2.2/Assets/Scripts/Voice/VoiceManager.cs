using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

public class VoiceManager : Singleton<VoiceManager>
{
    private KeywordRecognizer _recognizer;
    // Dictionary<string, Action> _actions = new Dictionary<string, Action>();
    protected VoiceInputList Actions = new VoiceInputList();
    
    public bool debugMode;

    
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        Actions.OnListChanged.AddListener(CreateNewRecognizer);
        CreateNewRecognizer();
    }

    private void CreateNewRecognizer()
    {
        //create new recognizer
        if (Actions.GetAllVoiceInputs().Count > 0)
        {
            _recognizer = new KeywordRecognizer(Actions.GetAllTriggers(), ConfidenceLevel.Low);
            _recognizer.OnPhraseRecognized += OnKeywordsRecognized;
            _recognizer.Start();
        }
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Assert(debugMode, "Command: " + args.text + ": Confidence " + args.confidence + ": time" + 
                                 args.phraseStartTime.ToString());
        Actions.GetVoiceInput(args.text).action?.Invoke();
    }

    public void AddVoiceCommand(string name, string trigger, Action action)
    {
    Actions.Add(name, trigger, action);
    }
    
    public void AddVoiceCommand(string name, Action action)
    {
        Actions.Add(name, action);
    }
        
    public void AddVoiceCommand(VoiceInput voiceInput)
    {
        Actions.Add(voiceInput);
    }
    
    public bool RemoveVoiceCommand(VoiceInput voiceInput)
    {
        return Actions.Remove(voiceInput);
    }
    public bool RemoveVoiceCommand(string name)
    {
        return Actions.Remove(name);
    }

    public VoiceInput GetVoiceCommand(string name)
    {
        return Actions.GetVoiceInput(name);
    }

    public List<VoiceInput> GetAllVoiceCommands()
    {
        return Actions.GetAllVoiceInputs();
    }

    public bool EditVoiceCommand(string name, string newTrigger = "", Action action = null)
    {
        if (newTrigger == "" && action == null)
        {
            //did nothing
            return false;
        }

        var voiceCommand = Actions.GetVoiceInput(name);
        Actions.Remove(name);
        if (newTrigger != "")
        {
            voiceCommand.trigger = newTrigger;
        }

        if (action != null)
        {
            voiceCommand.action = action;
        }
        Actions.Add(voiceCommand);
        return true;
    }
    
    public struct VoiceInput
    {
        public string name;
        public string trigger;
        public Action action;
    }

    protected class VoiceInputList
    {
        private Dictionary<string ,VoiceInput> _voiceInputs = new Dictionary<string, VoiceInput>();

        public void Add(VoiceInput voiceInput)
        {
            _voiceInputs.Add(voiceInput.name, voiceInput);
            OnListChanged.Invoke();
        }

        public void Add(string name, string trigger, Action action)
        {
            var voiceInput = new VoiceInput(){name = name, trigger = trigger, action = action};
            Add(voiceInput);
        }

        public void Add(string name, Action action)
        {
            var voiceInput = new VoiceInput() {name = name, trigger = name, action = action};
            Add(voiceInput);
        }

        public bool Remove(VoiceInput voiceInput)
        {
            var ret = _voiceInputs.Remove(voiceInput.name);
            OnListChanged.Invoke();
            return ret;
        }

        public bool Remove(string name)
        {
            var ret = _voiceInputs.Remove(name);
            OnListChanged.Invoke();
            return ret;
        }

        public VoiceInput GetVoiceInput(string name)
        {
            return _voiceInputs[name];
        }

        public List<VoiceInput> GetAllVoiceInputs()
        {
            return _voiceInputs.Values.ToList();
        }

        public string[] GetAllTriggers()
        {
            List<string> triggers = new List<string>();
            foreach (var voiceInput in _voiceInputs.Values)
            {
                triggers.Add(voiceInput.trigger);
            }

            return triggers.ToArray();
        }

        public void EditTrigger(VoiceInput voiceInput, string newTrigger)
        {
            Remove(voiceInput);
            var newAction = new VoiceManager.VoiceInput()
            {
                name = voiceInput.name,
                trigger = newTrigger,
                action = voiceInput.action
            };
            Add(newAction);
        }
        public UnityEvent OnListChanged = new UnityEvent();
    }
}
