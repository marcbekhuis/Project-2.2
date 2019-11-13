using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;



public class SaveManager : Singleton<SaveManager>
{
    public string[] levels;
    private string _scorePath;

    public SaveFile SaveData
    {
        get { return _saveFile; }
        set
        {
            _saveFile = value; 
            SaveSettings();
        }
    }
    [SerializeField]private SaveFile _saveFile;
    private readonly SaveFile.Level ERRORLEVEL;

    public string SaveFilePath
    {
        get { return _scorePath; }
    }

    [Serializable]
    public struct SaveFile
    {
        public string SaveFileVersion;
        public bool EnableTutorial;
        public bool IsFirstTime;
        public List<Level> levels;
        public List<PlayerStats> PlayerStatses;
        [System.Flags]
        public enum MiniGames
        {
            SkyLanders = 1
        }
        
        public SaveFile(string[] _levels)
        {
            levels = new List<Level>();
            for (int levelIndex = 0; levelIndex < _levels.Length; levelIndex++)
            {
                levels.Add(new Level(){LevelName = _levels[levelIndex], Unlocked = (levelIndex == 0 ? true : false)});
            }
            SaveFileVersion = "0.0.1";
            EnableTutorial = true;
            IsFirstTime = true;
            PlayerStatses = new List<PlayerStats>();
        }

        [Serializable]
        public struct Level
        {
            public string LevelName;
            public bool Unlocked;
            public MiniGames MiniGames; 
        }
        [Serializable]
        public struct PlayerStats
        {
            public string PlayerName;
            public double PlayerTime;
            public int PlayerScore;
            public MiniGames PlayedMiniGames;
            public string Level;
        }
    }

    public SaveManager()
    {
        ERRORLEVEL = new SaveFile.Level()
        {
            LevelName = "ERROR",
            Unlocked = false
        };
    }
    
    private void Awake()
    {
        _scorePath = Application.persistentDataPath + "/Score.bat";
        DontDestroyOnLoad(this);
        LoadSettings();
    }
    
    public void LoadSettings()
    {
        if (File.Exists(_scorePath))
        {
            try
            {
                using (var file = new StreamReader(File.OpenRead(_scorePath)))
                {
                    JsonUtility.FromJsonOverwrite(file.ReadToEnd(), SaveData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }    
        }
        else
        {
            //score does not exist create new 
            CreateNewFile(levels);
        }
    }

    public void SaveSettings()
    {
        if (SaveData.SaveFileVersion == "")
        {
            CreateNewFile(levels);
        }
        using (var file = new StreamWriter(File.Open(_scorePath, FileMode.Create)))
        {
            file.WriteLine(JsonUtility.ToJson(SaveData));
        }
        Debug.Log("Saved");
    }

    private void CreateNewFile(string[] levels)
    {
        SaveData = new SaveFile(levels);
        using (var file = new StreamWriter(File.Create(_scorePath)))
        {
            file.WriteLine(JsonUtility.ToJson(SaveData));
        }
    }

    private SaveFile.Level GetLevel(string levelName)
    {
        try
        {
            //Debug.Log(SaveData.levels.Count);
            return SaveData.levels.Single(save => save.LevelName == levelName);
        }
        catch (Exception e)
        {
            return ERRORLEVEL;
        }
        
    }

    private bool AddLevel(SaveFile.Level level)
    {
        try
        {
            SaveData.levels.Add(level);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }

    private bool RemoveLevel(string levelName)
    {
        return SaveData.levels.Remove(GetLevel(levelName));
    }

    public void UnLockLevel(string levelName)
    {
        var level = GetLevel(levelName);
        RemoveLevel(levelName);
        if (level.Equals(ERRORLEVEL)) return;
        level.Unlocked = true;
        AddLevel(level);
    }

    public bool AddScoreToLevel(SaveFile.PlayerStats score)
    {
        try
        {
            SaveData.PlayerStatses.Add(score);
            return true;
        }
        catch (Exception e)
        {
            return false;
        } 
    }

    public SaveFile.Level GetLevel(int levelIndex)
    {
        if (!(levelIndex < levels.Length))
        {
            Debug.LogError("Error " + levelIndex);
            return ERRORLEVEL;
        }
        

        return GetLevel(levels[levelIndex]);
    }
    
    public List<SaveFile.PlayerStats> GetScoresPerLevel(SaveFile.Level level)
    {
        if(level.LevelName == ERRORLEVEL.LevelName)
            return null;
        var playerstats = SaveData.PlayerStatses.Where(stats => stats.Level == level.LevelName);
        return playerstats.ToList();
    }
    public List<SaveFile.PlayerStats> GetScoresPerLevel(string level)
    {
        return GetScoresPerLevel(GetLevel(level));
    }
    
    
}
