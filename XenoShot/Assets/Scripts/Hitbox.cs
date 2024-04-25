using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Health health;
    bool isHead;
    // Start is called before the first frame update
    void Start()
    {
        isHead = false;
        if(transform.name == "Head")
        {
            isHead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRaycastHit(RaycastWeapon weapon, Vector3 direction)
    {
        health.TakeDamage(weapon.damage, direction, isHead);
    }
}
