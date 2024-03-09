using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public float maxHealth;
    public float dieForce;
    public bool isBoss; // Flag to identify if this entity is a boss
    private float currentHealth;

    private SpawnEnemy spawnEnemy; // Reference to the SpawnEnemy script

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Find and store reference to the SpawnEnemy script
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;

        if (currentHealth <= 0.0f)
        {
            Die(direction);
        }
    }

    private void Die(Vector3 force)
    {
        // If it's a boss, activate the win panel
        if (isBoss)
        {
            //spawnEnemy.BossDestroyed();
        }

        // Perform other death-related actions (e.g., play death animation, destroy GameObject)
        // ...
    }
}
