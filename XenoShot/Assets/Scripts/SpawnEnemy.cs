using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy Instance;
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public GameObject bossPrefab; // The boss prefab
    public GameObject winPanel; // Reference to the win panel
    public float spawnInterval = 2f; // Interval between enemy spawns
    public int maxEnemies = 10; // Maximum number of enemies to spawn
    public int bossSpawnThreshold = 0; // Threshold for spawning boss (when array is empty)
    public Transform[] spawnPoints; // Array of spawn points
    public int totalEnemyCount = 5; // Total number of enemies to spawn

    private GameObject[] enemies; // Array to hold spawned enemies
    private GameObject bossInstance; // Reference to the spawned boss
    public int enemyCount;
    public bool bossSpawned = false;

    GameObject Boss;
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
    void Start()
    {
        Instance = this;
        // Initialize the enemies array
        enemies = new GameObject[maxEnemies];

        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }
   

    IEnumerator SpawnEnemies()
    {
        int enemyCount = 0;
        int spawnPointIndex = 0;

        while (enemyCount < totalEnemyCount)
        {
            // Spawn an enemy at the current spawn point
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
            enemies[enemyCount] = enemy;
            enemyCount++;

            // Move to the next spawn point (cyclically)
            spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Length;

            // Wait for the spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }

        // Check if all enemies are dead
        while (!AllEnemiesDead())
        {
            yield return null;
        }

        // Spawn the boss if all enemies are dead
        SpawnBoss();
    }

    bool AllEnemiesDead()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnBoss()
    {
        // Spawn the boss enemy
        bossInstance = Instantiate(bossPrefab, transform.position, Quaternion.identity);
        bossSpawned=true;
    }

    public void Win()
    {
            winPanel.SetActive(true);
        
        

    }
    
}
