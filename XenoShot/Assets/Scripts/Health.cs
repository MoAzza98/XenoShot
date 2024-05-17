using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviour
{
    public static Health Instance;
    public GameObject damageNumber;
    public float currentHealth;
    public float maxHealth;
    UIHealthbar healthBar;
    EnemyLockOn enemyLockOn;
    AILocomotion aiLocomotion;
    public int scoreValue = 10; // Score value when an enemy is killed
    public bool isBoss; // Flag to identify if this entity is a boss
    private SpawnEnemy spawnEnemy; // Reference to the SpawnEnemy script
    //public GameObject winPanel; // Reference to the win panel
    public static Hitmarker hitmarker;
    private Image hitmarkerImage;

    public Transform targetTransform;

    [SerializeField] private GameObject hitmarkerIcon;
    [SerializeField] private Animator deathmarkerAnim;
    

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            hitmarker = hitmarkerIcon.GetComponent<Hitmarker>();
        } catch (Exception e)
        {
            
        }

        // Find and store reference to the SpawnEnemy script
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<UIHealthbar>();
        enemyLockOn = FindAnyObjectByType<EnemyLockOn>();
        aiLocomotion = GetComponent<AILocomotion>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();

        Debug.Log($"{gameObject} rigidbodies are {rigidBodies}");

        foreach (var rigidBody in rigidBodies)
        {
            Hitbox hitbox = rigidBody.gameObject.AddComponent<Hitbox>();
            hitbox.health = this;
            if(hitbox.gameObject != gameObject)
            {
                hitbox.gameObject.layer = LayerMask.NameToLayer("HitBox");
            }
        }

        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        //hitmarker.hitmarkerTimer =+ 0.1f;

        bool isCrit = (UnityEngine.Random.Range(0, 10) > 8);

        /*
        if(isHead)
        {
            amount = amount * 2;
        }*/

        if (isCrit) 
        { 
            amount = amount * 2; 
        }

        currentHealth -= amount;
        Debug.Log(transform.name + "deducted " + currentHealth + "HP");
        
        if (GetComponent<AiHealth>())
        {
            healthBar.SetHealthBarPercentage(currentHealth/maxHealth);
        }

        OnDamage(direction);

        //instantiate a damage number using amount float
        if(currentHealth > 0)
        {
            DamagePopup.Create(Vector3.zero, (int)amount, isCrit, false);
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
        OnDeath(force);
    }

    protected virtual void OnStart()
    {

    }
    protected virtual void OnDeath(Vector3 direction)
    {

    }
    protected virtual void OnDamage(Vector3 direction)
    {

    }



}
