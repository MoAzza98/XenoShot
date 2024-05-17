using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeapon : MonoBehaviour
{
    RaycastWeapon currentWeapon;
    Animator animator;
    MeshSockets sockets;
    WeaponIK weaponIK;
    Transform currentTarget;
    bool weaponActive = false;
    public float inaccuracy = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sockets = GetComponent<MeshSockets>();
        weaponIK = GetComponent<WeaponIK>();
    }

    private void Update()
    {
        if(currentTarget && currentWeapon && weaponActive)
        {
            Vector3 target = currentTarget.position + weaponIK.targetOffset;
            target += UnityEngine.Random.insideUnitSphere * inaccuracy;
            currentWeapon.UpdateFiring(Time.deltaTime, target);
        }

        try
        {
            currentWeapon.UpdateBullets(Time.deltaTime);
        } catch(Exception e)
        {

        }
    }

    public void SetFiring(bool enabled)
    {
        Vector3 target = currentTarget.position + weaponIK.targetOffset;
        if (enabled)
        {
            currentWeapon.StartFiring(target);
        }
        else
        {
            currentWeapon.StopFiring();
        }
    }

    public void Equip(RaycastWeapon weapon)
    {
        if (GetComponentInChildren<RaycastWeapon>())
        {

            return;
        }
        currentWeapon = weapon;
        sockets.Attach(weapon.transform, MeshSockets.SocketId.RightHand);
    }

    public void ActivateWeapon()
    {
        StartCoroutine(EquipWeapon());
    }

    IEnumerator EquipWeapon()
    {
        animator.SetBool("Equip", true);
        yield return new WaitForSeconds(0.5f);

        weaponIK.SetAimTransform(currentWeapon.raycastOrigin);
        weaponActive = true;
    }

    public void SetTarget(Transform target)
    {
        weaponIK.SetTargetTransform(target);
        currentTarget = target;
    }

    public bool HasWeapon()
    {
        return currentWeapon != null;
    }
}
