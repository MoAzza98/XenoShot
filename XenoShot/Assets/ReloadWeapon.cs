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


    // Start is called before the first frame update
    void Start()
    {
        wEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rigController.SetTrigger("ReloadWeapon");
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
        throw new NotImplementedException();
    }

    private void DetachMagazine()
    {
        throw new NotImplementedException();
    }

    private void DropMagazine()
    {
        throw new NotImplementedException();
    }

    private void RefillMagazine()
    {
        throw new NotImplementedException();
    }
}
