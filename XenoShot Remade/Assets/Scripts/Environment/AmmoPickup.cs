using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int clipAmount = 2;

    private void OnTriggerEnter(Collider other) {
        ActiveWeapon playerWeapon = other.GetComponent<ActiveWeapon>();
        if (playerWeapon) {
            playerWeapon.RefillAmmo(clipAmount);
            Destroy(gameObject);
        }

        AiWeapons aiWeapons = other.GetComponent<AiWeapons>();
        if (aiWeapons && aiWeapons.IsLowAmmo()) {
            aiWeapons.RefillAmmo(clipAmount);
            Destroy(gameObject);
        }
    }
}
