using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public static Health Instance;
    [HideInInspector]
    public float currentHealth;
    public float maxHealth;
    public float dieForce;
    Ragdoll ragdoll;
    NavMeshAgent agent;
    UIHealthbar healthBar;
    EnemyLockOn enemyLockOn;
    AILocomotion aiLocomotion;
    public int scoreValue = 10; // Score value when an enemy is killed
    public bool isBoss; // Flag to identify if this entity is a boss
    private SpawnEnemy spawnEnemy; // Reference to the SpawnEnemy script
    public GameObject winPanel; // Reference to the win panel
    

    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;

        // Find and store reference to the SpawnEnemy script
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        currentHealth = maxHealth;
        ragdoll = GetComponent<Ragdoll>();
        agent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<UIHealthbar>();
        enemyLockOn = FindAnyObjectByType<EnemyLockOn>();
        aiLocomotion = GetComponent<AILocomotion>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach ( var rigidBody in rigidBodies)
        {
            Hitbox hitbox = rigidBody.gameObject.AddComponent<Hitbox>();
            hitbox.health = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        Debug.Log(currentHealth);
        healthBar.SetHealthBarPercentage(currentHealth/maxHealth);
        if(currentHealth <= 0.0f)
        {
            Die(direction);
        }
    }

    private void Die(Vector3 force)
    {
        if (isBoss)
        {
            SpawnEnemy.Instance.Win();
        }
        // Increase the score when an enemy dies
        ScoreManager.instance.AddEnemyKillScore(); // Call AddEnemyKillScore() method from ScoreManager

        // Perform other death-related actions (e.g., play death animation, destroy GameObject)
        aiLocomotion.enabled = false;
        enemyLockOn.ResetTarget();
        healthBar.gameObject.SetActive(false);
        ragdoll.ActivateRagdoll();
        force.y = 1;
        ragdoll.ApplyForce(force * dieForce);
        agent.enabled = false;
        Destroy(gameObject, 1f);
        SpawnEnemy.Instance.bossSpawned = false;
        
    }

}
