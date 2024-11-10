using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static event Action<int> OnScoreChange = delegate { };
    public static int score;

    int bestScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private void Start()
    {
        score = 0;
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        UpdateScoreUI();
    }

    private void Update()
    {
        if (scoreText != null && scoreText.text != score.ToString())
        {
            scoreText.text = score.ToString();
            NotifyScoreChange();
        }

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", bestScore);
            PlayerPrefs.Save();
            bestScoreText.text = bestScore.ToString();
        }
    }

    // Update the UI when the score changes
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
            bestScoreText.text = bestScore.ToString();
        }
    }

    private void NotifyScoreChange()
    {
        OnScoreChange?.Invoke(score);
    }
}