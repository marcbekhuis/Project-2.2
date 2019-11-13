using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public int playerNameLength = 10;
    [Space(3)]
    public Text levelText;
    public Text[] highScoreText;
    public Image lockedImage;
    public Image unlockedImage;

    public SaveManager.SaveFile.Level level;
    public List<SaveManager.SaveFile.PlayerStats> scores = new List<SaveManager.SaveFile.PlayerStats>();

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        UpdateValues();
    }

    public void UpdateGraphics()
    {
        levelText.text = level.LevelName;
        lockedImage.gameObject.SetActive(!level.Unlocked);
        unlockedImage.gameObject.SetActive(level.Unlocked);
        if (scores != null)
        {
            for (int i = 0; i < highScoreText.Length; i++)
            {
                highScoreText[i].text = "";
            }
            for (int i = 0; i < Mathf.Min(3, scores.Count); i++)
            {
                highScoreText[i].text = $@"1e: {scores[i].PlayerName.Truncate(playerNameLength)}, Score: {scores[i].PlayerScore}";
            }
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
            scores = tempscores.OrderBy(x => x.PlayerScore).ToList();
        }
        
        UpdateGraphics();
    }
}
