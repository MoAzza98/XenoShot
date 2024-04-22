using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEditor.Animations;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crossHairTarget;
    public UnityEngine.Animations.Rigging.Rig handIK;
    public Transform weaponParent;
    public Transform lGrip;
    public Transform rGrip;

    RaycastWeapon weapon;
    Animator animator;
    AnimatorOverrideController overrideController;

    public TwoBoneIKConstraint lArm;
    public TwoBoneIKConstraint rArm;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            EquipWeapon(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                weapon.StartFiring();
            }
            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                weapon.StopFiring();
            }
        }
        else
        {
            handIK.weight = 0f;
            animator.SetLayerWeight(1, 0.0f);
        }
    }

    public void EquipWeapon(RaycastWeapon newWeapon)
    {
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }
        if(newWeapon.gunType == GunType.OneHanded)
        {
            lArm.weight = 0f;
        } else
        {
            lArm.weight = 1f;
        }
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        //weapon.transform.parent = weaponParent;
        //weapon.transform.localPosition = Vector3.zero;
        //weapon.transform.localRotation = Quaternion.identity;

        handIK.weight = 1f;
        animator.SetLayerWeight(1, 1.0f);
        Invoke(nameof(SetAnimationDelayed), 0.00001f);
    }

    void SetAnimationDelayed()
    {
        overrideController["weapon_anim_Empty"] = weapon.weaponAnimation;
    }

    [ContextMenu("Save Weapon Pose")]
    void SaveWeaponPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
        recorder.BindComponentsOfType<Transform>(lGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(rGrip.gameObject, false);

        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(weapon.weaponAnimation);
        UnityEditor.AssetDatabase.SaveAssets();
    }
}
