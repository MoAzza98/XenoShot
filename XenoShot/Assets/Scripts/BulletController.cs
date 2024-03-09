using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Perform actions when the bullet hits the player
            Debug.Log("Bullet hit the player!");
            Destroy(gameObject); // Destroy the bullet on collision
        }
    }
}
