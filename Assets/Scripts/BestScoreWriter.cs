using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreWriter : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    private int bestScore;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        UploadBestScore();
    }

    private void UploadBestScore()
    {
        bestScoreText.text = bestScore.ToString();
    }
}