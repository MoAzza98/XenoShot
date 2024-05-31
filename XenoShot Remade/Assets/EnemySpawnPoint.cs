using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemy;
    private bool canSpawnEnemy = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canSpawnEnemy)
        {
            canSpawnEnemy = false;
            GameObject newEnemy = Instantiate(enemy, gameObject.transform);
        }
    }
}
