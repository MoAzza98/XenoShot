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
    public AmmoWidget ammoWidget;
    private ReloadWeapon reload;

    RaycastWeapon weapon;
    Animator animator;
    AnimatorOverrideController overrideController;

    public TwoBoneIKConstraint lArm;
    public TwoBoneIKConstraint rArm;

    // Start is called before the first frame update
    void Start()
    {
        reload = GetComponent<ReloadWeapon>();
        animator = GetComponent<Animator>();
        overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            EquipWeapon(existingWeapon);
        }
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return weapon;
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !reload.isReloading)
            {
                if (!weapon.isFiring)
                {
                    weapon.StartFiring(crossHairTarget.position);
                }
                else
                {
                    weapon.UpdateFiring(Time.deltaTime, crossHairTarget.position);
                }
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

        weapon.UpdateBullets(Time.deltaTime);
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
        //weapon.transform.parent = weaponParent;
        //weapon.transform.localPosition = Vector3.zero;
        //weapon.transform.localRotation = Quaternion.identity;

        handIK.weight = 1f;
        animator.SetLayerWeight(1, 1.0f);
        Invoke(nameof(SetAnimationDelayed), 0.00001f);

        ammoWidget.RefreshAmmo(weapon.ammoCount);
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
