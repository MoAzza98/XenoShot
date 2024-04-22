using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the slider representing player's health
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Update the health UI at the start
    }

    // Method to reduce player's health
    public void ReduceHealth(float amount)
    {
        currentHealth -= amount;

        // Update the health UI
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to update the health UI
    private void UpdateHealthUI()
    {
        // Set the slider value based on current health
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("Health slider reference is not set in PlayerHealth script.");
        }
    }

    // Method to handle player's death
    private void Die()
    {
        // Your logic to handle player's death (e.g., respawn, game over)
        Debug.Log("Player died.");
    }
}
