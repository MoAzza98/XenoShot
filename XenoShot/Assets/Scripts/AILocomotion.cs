using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    private Transform playerTransform;
    public float characterHeight;
    NavMeshAgent agent;
    Animator animator;

    public float maxTime = 1.0f;
    public float maxdistance = 1.0f;
    private float timer = 0.0f;
    public float shootRange = 5.0f;
    public float maxDistanceToPlayer = 1.0f;
    public float rotationSpeed = 88.04f; // Rotation speed around the y-axis


    public GameObject bulletPrefab; // Assign bullet prefab in the Inspector
    public Transform bulletSpawnPoint; // Assign bullet spawn point in the Inspector
    public GameObject hitParticlePrefab; // Assign hit particle prefab in the Inspector
    public float bulletSpeed = 10f; // Speed of the bullet
    private float bulletTimer = 0.4f; // Timer for bullet spawning
    public HealthBar playerHealth; // Reference to the player's health component
    public float hitParticleSpawnInterval = 1.0f; // Interval between spawning hit particles
    private float hitParticleTimer = 0.0f; // Timer for hit particle spawning


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("MegaBoy").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTransform.position;
        // Get the PlayerHealth component from the player object
        playerHealth = playerTransform.GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        hitParticleTimer -= Time.deltaTime;
        if (hitParticleTimer <= 0)
        {
            // Spawn hit particle
            SpawnHitParticle();
            hitParticleTimer = hitParticleSpawnInterval; // Reset timer
        }
        // Reduce bullet timer
        bulletTimer -= Time.deltaTime;
        timer -= Time.deltaTime;
        float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= shootRange)
            {
                animator.SetBool("Shoot", true);
                agent.isStopped = true;

                Vector3 directionToPlayer = playerTransform.position - transform.position;
                directionToPlayer.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Spawn bullet if the bullet timer reaches 0 and reset it
                if (bulletTimer <= 0)
                {
                    ShootBullet(directionToPlayer.normalized);
                    bulletTimer = 0.4f; // Reset the timer

                    // Reduce player's health when shooting
                    if (playerHealth != null)
                    {
                        playerHealth.ReduceHealth(0.5f);
                    }
                }
            }
            else
            {
                animator.SetBool("Shoot", false);
                agent.isStopped = false;
                agent.destination = playerTransform.position;
            }

            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    void ShootBullet(Vector3 direction)
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = direction * bulletSpeed;
            }
            else
            {
                Debug.LogError("Bullet prefab does not have a Rigidbody component!");
                return;
            }

            // Set up collision detection for the bullet
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.SetTarget(playerTransform);
            }
            else
            {
                Debug.LogError("Bullet prefab does not have a BulletController component!");
            }
        }
        else
        {
            Debug.LogError("Bullet prefab or bullet spawn point is not assigned in AILocomotion script!");
        }
    }
    void SpawnHitParticle()
    {
        if (hitParticlePrefab != null)
        {
            // Instantiate hit particle at the current position
            Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Hit particle prefab is not assigned in AILocomotion script!");
        }
    }
}