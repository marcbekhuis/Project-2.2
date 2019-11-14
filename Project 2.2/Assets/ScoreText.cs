using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text text;
    
    private void OnEnable()
    {
        text.text = "Your score is: " + LevelScore.Instance.Score;
    }
}
