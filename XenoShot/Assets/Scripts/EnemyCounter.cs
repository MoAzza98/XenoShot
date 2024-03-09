using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public Text enemyCountText; // Reference to the UI Text component to display enemy count
    private int totalEnemiesSpawned; // Counter to keep track of total enemies spawned
    private int enemiesDestroyed; // Counter to keep track of enemies destroyed

    void Start()
    {
        totalEnemiesSpawned = 0;
        enemiesDestroyed = 0;
        UpdateEnemyCountText();
    }

    void EnemySpawned()
    {
        // Increment the total enemies spawned counter
        totalEnemiesSpawned++;
        UpdateEnemyCountText();
    }

    void EnemyDestroyed()
    {
        // Increment the enemies destroyed counter
        enemiesDestroyed++;
        UpdateEnemyCountText();
    }

    void UpdateEnemyCountText()
    {
        // Update the UI Text to display the current enemy count
        enemyCountText.text = "Enemies: " + (totalEnemiesSpawned - enemiesDestroyed).ToString();
    }
}
