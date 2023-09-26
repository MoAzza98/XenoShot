using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [HideInInspector]
    public float currentHealth;
    public float maxHealth;
    public float dieForce;
    Ragdoll ragdoll;
    NavMeshAgent agent;
    UIHealthbar healthBar;
    EnemyLockOn enemyLockOn;
    AILocomotion aiLocomotion;

    // Start is called before the first frame update
    void Start()
    {
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
        aiLocomotion.enabled = false;
        enemyLockOn.ResetTarget();
        healthBar.gameObject.SetActive(false);
        ragdoll.ActivateRagdoll();
        force.y = 1;
        ragdoll.ApplyForce(force * dieForce);
        agent.enabled = false;
        Destroy(gameObject, 5f);
    }
}
