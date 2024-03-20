using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
 
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public GameObject enemyPrefab; // Enemy prefab to be spawned
    public float spawnInterval = 5f; // Time interval between enemy spawns
    public float spawnRadius = 5f; // Radius around the player where enemies can spawn
    private Transform player; // Reference to the player's transform
    int count = 0;
    bool stopSpawning = true;
    public int totalEnemies;
    public GameObject Winpanel;
    public GameObject playercam;
    public GameObject bossPrefab; // Boss prefab to be spawned
     
    private int totalEnemiesToSpawn = 10;
    private int activeEnemies = 0;
    private bool spawningEnemies = true;
    private List<GameObject> enemies = new List<GameObject>(); // List to keep track of spawned enemies
    private GameObject bossInstance; // Reference to the spawned boss
   

    public void WinP()
    {
        Winpanel.SetActive(true);
        Time.timeScale = 0;
        playercam.SetActive(false);
    }
    void Update()
    {
        // Check if all enemies and boss are inactive
        if (!spawningEnemies && AllEnemiesInactive() && !IsBossActive())
        {
            // Activate the win panel
            WinP();
        }
    }
    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player in the scene
        StartCoroutine(SpawnEnemiesRepeatedly());
    }

    IEnumerator SpawnEnemiesRepeatedly()
    {
        while (stopSpawning)
        {
            count++;
          
            SpawnEnemyAroundPlayer();
            if (totalEnemies == count)
            {
                stopSpawning = false;
                SpawnBossEnemyAroundPlayer();
               
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemyAroundPlayer()
    {
        Vector3 spawnPosition = player.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0f; // Ensure enemies spawn at the same height as the player (assuming y = 0 is the ground)
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    void SpawnBossEnemyAroundPlayer()
    {
        Vector3 spawnPosition = player.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0f; // Ensure enemies spawn at the same height as the player (assuming y = 0 is the ground)
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
    bool AllEnemiesInactive()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.activeInHierarchy)
                return false; // At least one enemy is still active
        }
        return true; // All enemies are inactive
    }

    bool IsBossActive()
    {
        return bossInstance != null && bossInstance.activeInHierarchy;
    }
}
