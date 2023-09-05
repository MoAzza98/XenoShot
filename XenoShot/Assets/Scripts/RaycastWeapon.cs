using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{

    public bool isFiring = false;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public Transform raycastOrigin;
    public Transform raycastDestination;
    public TrailRenderer tracerEffect;

    public bool isAuto;
    public int fireRate = 25;
    public float damage = 10;
    float accumulatedTime;

    Ray ray;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        raycastDestination = GameObject.Find("CrossHairTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 testRay = raycastOrigin.TransformDirection(Vector3.down) * 20;
        //Debug.DrawRay(raycastOrigin.position, testRay, Color.green);
    }

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0f;
        FireBullet();
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while(accumulatedTime > 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    private void FireBullet()
    {
        muzzleFlash.Emit(1);

        //fix this lmao
        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracerEffect.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);

            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
            //Debug.Log("Spawned tracer... I think: " + tracer.transform.position);

            var rb2d = hitInfo.collider.GetComponent<Rigidbody>();
            if (rb2d)
            {
                rb2d.AddForceAtPosition(ray.direction * 20, hitInfo.point, ForceMode.Impulse);
            }

            var hitbox = hitInfo.collider.GetComponent<Hitbox>();
            if (hitbox)
            {
                hitbox.OnRaycastHit(this, ray.direction);
            }
        }
    }

    public void StopFiring()
    {
        isFiring= false;
    }

}
