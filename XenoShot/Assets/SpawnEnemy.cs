using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = Instantiate(enemy);
        enemy.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
