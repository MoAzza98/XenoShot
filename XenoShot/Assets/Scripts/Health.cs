using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public static Health Instance;
    public GameObject damageNumber;
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
    //public GameObject winPanel; // Reference to the win panel

    public Transform targetTransform;
    

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
        bool isCrit = (Random.Range(0, 10) > 8);

        if (isCrit) 
        { 
            amount = amount * 2; 
        }
        currentHealth -= amount;
        Debug.Log(currentHealth);
        healthBar.SetHealthBarPercentage(currentHealth/maxHealth);

        //instantiate a damage number using amount float
        if(currentHealth > 0)
        {

            DamagePopup.Create(Vector3.zero, (int)amount, isCrit);
        }


        if(currentHealth <= 0.0f)
        {
            Die(direction);
        }
    }

    void ShowDamageNumber(float amount)
    {
        var go = Instantiate(damageNumber, (targetTransform.position - Camera.main.transform.position).normalized, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = amount.ToString();
    }

    private void Die(Vector3 force)
    {
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
        Destroy(gameObject, 2f);
        

        // Check if this entity is a boss and if its health has reached zero
        if (isBoss && currentHealth <= 0)
        {
            Time.timeScale = 0;

            SceneManager.LoadScene("WinPanel");
            
        }
    }


}
