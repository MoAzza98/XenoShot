using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    public float destroyTime;
    Vector3 offset = new Vector3(0, 1.2f, 0);
    public Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, destroyTime);
        //transform.position += offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*
        Vector3 direction = (targetTransform.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f;

        transform.position = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
        */
    }
}
