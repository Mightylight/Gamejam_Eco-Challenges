using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreCounter : MonoBehaviour
{
    
    public int highscore = 0;
    [SerializeField] private GameObject gameoverMenu;
    [SerializeField] private TMP_Text gameOverHighscoreText;

    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private Slider highscoreSlider;
    
    
    
    
    
    private static HighscoreCounter instance;
    
    public static HighscoreCounter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HighscoreCounter>();
            }

            return instance;
        }
    }

    private void Update()
    {
        highscoreText.text = $"Score: {highscore}";
        if (highscore > highscoreSlider.maxValue)
        {
            highscoreSlider.value = highscoreSlider.maxValue;
        }
        else
        {
            highscoreSlider.value = highscore;
        }
    }

    public void AddToHighscore(int points)
    {
        highscore += points;
    }

    public void ResetHighscore()
    {
        highscore = 0;
    }

    private void Awake()
    {
        ResetHighscore();
    }

    public void GameOver()
    {
       string highscoreString =  PlayerPrefs.GetString("HighScores", "");
       string[] split = highscoreString.Split(',');
       if(split.Length == 0)
       {
           PlayerPrefs.SetString("HighScores", highscore.ToString());
           PlayerPrefs.Save();
           Debug.Log("No other highscores found");
           return;
       }

       List<int> highscores = new();

       foreach (string splitPiece in split)
       {
           int.TryParse(splitPiece, out int score);
           if (score != 0)
           {
               highscores.Add(score);
           }
       }
       Debug.Log("Highscores: " + string.Join(",", highscores));
       
       highscores.Add(highscore);
       highscores.Sort((a, b) => b.CompareTo(a));
       if (highscores.Count > 5)
       {
           highscores = highscores.GetRange(0, 5);
       }
       
       Debug.Log("Highscores: " + string.Join(",", highscores));
       PlayerPrefs.SetString("HighScores", string.Join(",", highscores));
       
       //Do popup with highscores
       gameoverMenu.SetActive(true);
       gameOverHighscoreText.text = $"Highscore: {highscore}\n";
    }
    
    public void ToMainMenu()
    {
        gameoverMenu.SetActive(false);
        ResetHighscore();
        Debug.Log("I am here");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
