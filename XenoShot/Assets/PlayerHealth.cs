using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    Ragdoll ragdoll;
    ActiveWeapon weapons;
    protected override void OnStart()
    {
        ragdoll = GetComponent<Ragdoll>();
        weapons = GetComponent<ActiveWeapon>();
    }
    protected override void OnDeath(Vector3 direction)
    {
        Debug.Log("Player should die");
        ragdoll.ActivateRagdoll();
    }
    protected override void OnDamage(Vector3 direction)
    {

    }
}
