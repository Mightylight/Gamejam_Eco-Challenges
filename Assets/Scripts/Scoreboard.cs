using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    private int maxScores = 5; 

    [SerializeField]
    private List<int> highScores = new List<int>(); 

    public TMP_Text highScoreText;

    private void Start()
    {
        LoadHighScores();
        UpdateHighScoreUI();
    }

    public void UpdateHighScore(int newScore)
    {
        highScores.Add(newScore);

        highScores.Sort((a, b) => b.CompareTo(a));

        if (highScores.Count > maxScores)
        {
            highScores = highScores.GetRange(0, maxScores);
        }

        SaveHighScores();

        UpdateHighScoreUI();
    }

    private void LoadHighScores()
    {
        string highScoresString = PlayerPrefs.GetString("HighScores", "");
        if (!string.IsNullOrEmpty(highScoresString))
        {
            highScores = new List<int>(Array.ConvertAll(highScoresString.Split(','), int.Parse));
        }
    }

    private void SaveHighScores()
    {
        string highScoresString = string.Join(",", highScores);
        PlayerPrefs.SetString("HighScores", highScoresString);
        PlayerPrefs.Save();
    }

    private void UpdateHighScoreUI()
    {
        string highScoreString = "High Scores:\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            highScoreString += (i + 1) + ". " + highScores[i] + "\n";
        }
        highScoreText.text = highScoreString;
    }
}
