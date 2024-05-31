using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBounds : MonoBehaviour
{
    public Transform max;
    public Transform min;

    public Vector3 RandomPosition() {
        Vector3 randomPosition = new Vector3(
            Random.Range(min.position.x, max.position.x),
            Random.Range(min.position.y, max.position.y),
            Random.Range(min.position.z, max.position.z)
            );
        return randomPosition;
    }
}
