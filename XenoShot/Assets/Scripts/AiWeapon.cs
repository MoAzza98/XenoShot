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

    private void Start()
    {
        animator = GetComponent<Animator>();
        sockets = GetComponent<MeshSockets>();
        weaponIK = GetComponent<WeaponIK>();
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
