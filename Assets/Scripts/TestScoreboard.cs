using UnityEngine;

public class TestScoreboard : MonoBehaviour
{
    public Scoreboard scoreboard;

    void Start()
    {
        // Simulate test data
        int[] testScores = { 1500, 2000, 1800, 1700, 2200 };

        // Update high scores using test data
        foreach (int score in testScores)
        {
            scoreboard.UpdateHighScore(score);
        }
    }
}
