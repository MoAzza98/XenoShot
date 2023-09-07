using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    private Rigidbody rb;
    bool collisionOccured;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collisionOccured)
        {
            collision.transform.root.TryGetComponent<Health>(out var health); health.TakeDamage(damage, transform.position);
            collisionOccured = true;
        }
    }

}
