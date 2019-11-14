using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int score = 0;

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
}
