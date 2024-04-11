using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreCounter : MonoBehaviour
{
    
    public int highscore = 0;
    
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
}
