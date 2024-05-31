using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    public bool debug;
    Camera mainCamera;
    Ray ray;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;
        if (Physics.Raycast(ray, out hitInfo)) {
            transform.position = hitInfo.point;
        } else {
            transform.position = ray.origin + ray.direction * 1000.0f;
        }
    }

    private void OnDrawGizmos() {
        if (debug) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction);
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}
