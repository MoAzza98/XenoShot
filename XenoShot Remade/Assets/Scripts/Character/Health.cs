using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float lowHealth = 20;

    SkinnedMeshRenderer skinnedMeshRenderer;
    UIHealthBar healthBar;
    
    public float blinkIntensity = 10;


    public float blinkDuration = 0.05f;
    float blinkTimer;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidBody in rigidBodies) {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
            if (hitBox.gameObject != gameObject) {
                hitBox.gameObject.layer = LayerMask.NameToLayer("Hitbox");
            }
        }

        OnStart();
    }

    public void Heal(float amount) {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        if (healthBar) {
            healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        }
        OnHeal(amount);
        
        blinkTimer = blinkDuration;
    }

    public void TakeDamage(float amount, Vector3 direction) {
        currentHealth -= amount;
        if (healthBar) {
            healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        }
        OnDamage(direction);
        if (currentHealth <= 0.0f) {
            Die(direction);
        }

        blinkTimer = blinkDuration;
    }

    public bool IsDead() {
        return currentHealth <= 0;
    }

    public bool IsLowHealth() {
        return currentHealth < lowHealth;
    }

    private void Die(Vector3 direction) {
        OnDeath(direction);
    }

    private void Update() {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }

    protected virtual void OnStart() {

    }

    protected virtual void OnDeath(Vector3 direction) {

    }

    protected virtual void OnDamage(Vector3 direction) {

    }

    protected virtual void OnHeal(float amount) {

    }
}
