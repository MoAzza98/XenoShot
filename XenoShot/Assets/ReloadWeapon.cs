using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvents wEvents;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;
    public AmmoWidget ammoWidget;
    public bool isReloading;

    public GameObject magazineItem;

    private GameObject activeMag;


    // Start is called before the first frame update
    void Start()
    {
        wEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            if (Input.GetKeyDown(KeyCode.R) || weapon.ammoCount <=0)
            {
                rigController.SetTrigger("ReloadWeapon");
                isReloading = true;
            }

            if (weapon.isFiring)
            {
                ammoWidget.RefreshAmmo(weapon.ammoCount);
            }
        }
    }

    void OnAnimationEvent(string eventName)
    {
        Debug.Log(eventName);
        switch (eventName)
        {
            case "detachCanister":
                DetachMagazine();
                break;

            case "dropCanister":
                DropMagazine();
                break;

            case "refillCanister":
                RefillMagazine();
                break;

            case "attachCanister":
                AttachMagazine();
                break;
        }
    }

    private void AttachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        activeMag.SetActive(false);
        weapon.ammoCount = weapon.clipSize;
        rigController.ResetTrigger("ReloadWeapon");
        isReloading = false;

        ammoWidget.RefreshAmmo(weapon.ammoCount);
    }

    private void DetachMagazine()
    {
        if(activeMag == null)
        {
            activeMag = Instantiate(magazineItem);

            activeMag.transform.parent = leftHand;
            activeMag.transform.localPosition = Vector3.zero;
            activeMag.transform.rotation = leftHand.rotation;
        }
    }

    private void DropMagazine()
    {
        activeMag.SetActive(false);
        GameObject droppedMag = Instantiate(magazineItem, leftHand.transform.position, leftHand.transform.rotation);
        droppedMag.AddComponent<Rigidbody>();
        droppedMag.AddComponent<BoxCollider>();
        Destroy(droppedMag, 1.0f);
    }

    private void RefillMagazine()
    {
        activeMag.SetActive(true);
    }
}
