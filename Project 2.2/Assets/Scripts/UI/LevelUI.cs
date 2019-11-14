using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Text[] highScoreText;

    public SaveManager.SaveFile.Level level;

    [Space(3)] public Text levelText;
    public OpenSceneScript sceneScript;
    public VoiceCommandEvent voiceCommand;
    public int playerNameLength = 10;
    public List<SaveManager.SaveFile.PlayerStats> scores = new List<SaveManager.SaveFile.PlayerStats>();
    public Image unlockedImage;
    public Image lockedImage;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        UpdateValues();
    }

    private void OnEnable()
    {
        UpdateValues();
    }

    public void UpdateGraphics()
    {
        levelText.text = level.LevelName;
        lockedImage.gameObject.SetActive(!level.Unlocked);
        unlockedImage.gameObject.SetActive(level.Unlocked);
        if (scores != null)
        {
            for (var i = 0; i < highScoreText.Length; i++) highScoreText[i].text = "";
            for (var i = 0; i < Mathf.Min(3, scores.Count); i++)
                highScoreText[i].text =
                    $@"{i +1 }e: {scores[i].PlayerName.Truncate(playerNameLength)}, Score: {scores[i].PlayerScore}";
        }
    }

    public void UpdateValues()
    {
        level = SaveManager.Instance.GetLevel(transform.GetSiblingIndex());
        //Debug.Log(transform.GetSiblingIndex());
        //Debug.Log(level.LevelName);
        var tempscores = SaveManager.Instance.GetScoresPerLevel(level);
        //Debug.Log(tempscores);
        if (tempscores?.Count > 0)
        {
            scores = tempscores.OrderBy(x => x.PlayerScore * -1).ThenBy(x => x.PlayerTime * -1).ToList();
            //Debug.Log(scores[0].PlayerName);
        }

        voiceCommand.voiceCommandName = level.LevelName;
        voiceCommand.voiceCommandtrigger = level.LevelName;
        voiceCommand.UpdateCommand();
        sceneScript.levelName = level.LevelName;
        UpdateGraphics();
    }
}