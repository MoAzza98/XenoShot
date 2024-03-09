using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText; // Reference to the UI Text element to display the score
    private int score = 0;

    private void Start()
    {
        instance = this;
        UpdateScoreUI(); // Update the score UI at the start
    }

    // Method to add score
    public void AddScore(int amount)
    {
        score += amount;

        // Update the score UI
        UpdateScoreUI();
    }

    // Method to add 10 scores when an enemy dies
    public void AddEnemyKillScore()
    {
        // Add 10 to the score
        AddScore(10);
    }

    // Method to update the score UI
    private void UpdateScoreUI()
    {
        // Update the UI Text element with the current score
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score text reference is not set in ScoreManager script.");
        }
    }
}
