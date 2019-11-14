using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelScore : Singleton<LevelScore>
{
    [SerializeField] private Text scoreText;
    private int score = 0;
    public string playerName;

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }

    private void OnDestroy()
    {
        var playerStats = new SaveManager.SaveFile.PlayerStats()
        {
            PlayerName = playerName,
            PlayerScore = score,
            Level = SceneManager.GetActiveScene().name,
            PlayedMiniGames = SaveManager.SaveFile.MiniGames.None,
            PlayerTime = Time.timeSinceLevelLoad
        };

        SaveManager.Instance.AddScoreToLevel(playerStats);
    }
}
