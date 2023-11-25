using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTracer : MonoBehaviour
{
    private float destroyTimer = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer -= Time.deltaTime;

        if (destroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
