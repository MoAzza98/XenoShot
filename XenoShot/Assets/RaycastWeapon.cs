using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{

    public bool isFiring = false;
    public ParticleSystem muzzleFlash;
    public Transform raycastOrigin;

    Ray ray;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 testRay = raycastOrigin.TransformDirection(Vector3.down) * 20;
        Debug.DrawRay(raycastOrigin.position, testRay, Color.green);
    }

    public void StartFiring()
    {
        isFiring = true;
        muzzleFlash.Emit(1);

        //fix this lmao
        ray.origin = raycastOrigin.position;
        ray.direction = raycastOrigin.TransformDirection(Vector3.down);
        if(Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawRay(ray.origin, hitInfo.point, Color.red, 10.0f);
        }
    }

    public void StopFiring()
    {
        isFiring= false;
    }

}
