using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float amount = 50;

    private void OnTriggerEnter(Collider other) {
        Health health = other.GetComponent<Health>();
        if (health && health.IsLowHealth()) {
            health.Heal(amount);
            Destroy(gameObject);
        }
    }
}
