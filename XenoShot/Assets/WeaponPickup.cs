using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponFab;
    public DisplayWeaponPickup pickup;
    public string weaponName;

    public bool isCommon;
    public bool isRare;
    public bool isEpic;
    public bool isLegendary;

    private void OnTriggerEnter(Collider other)
    {
        if (isCommon)
        {
            pickup.DisplayPickupText(1, weaponName);
        } 
        else if (isRare)
        {
            pickup.DisplayPickupText(2, weaponName);
        }
        else if (isEpic)
        {
            pickup.DisplayPickupText(3, weaponName);
        }
        else if (isLegendary)
        {
            pickup.DisplayPickupText(4, weaponName);
        }

        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();

        if (activeWeapon)
        {
            RaycastWeapon weapon = Instantiate(weaponFab, activeWeapon.weaponParent);
            activeWeapon.EquipWeapon(weapon);
        }
    }
}
