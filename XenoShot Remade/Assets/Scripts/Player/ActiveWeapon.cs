using System;
using System.Collections;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot {
        Primary = 0,
        Secondary = 1
    }

    public Animator rigController;
    public Transform[] weaponSlots;
    public bool isChangingWeapon;

    RaycastWeapon[] equipped_weapons = new RaycastWeapon[2];
    CharacterAiming characterAiming;
    AmmoWidget ammoWidget;
    Transform crossHairTarget;
    ReloadWeapon reload;

    int activeWeaponIndex = -1;
    bool isHolstered = false;

    private void Awake() {
        crossHairTarget = Camera.main.transform.Find("CrossHairTarget");
        ammoWidget = FindObjectOfType<AmmoWidget>();
        characterAiming = GetComponent<CharacterAiming>();
        reload = GetComponent<ReloadWeapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon) {
            Equip(existingWeapon);
        }
    }

    public bool IsFiring() {
        RaycastWeapon currentWeapon = GetActiveWeapon();
        if (!currentWeapon) {
            return false;
        }
        return currentWeapon.isFiring;
    }

    public RaycastWeapon GetActiveWeapon() {
        return GetWeapon(activeWeaponIndex);
    }

    RaycastWeapon GetWeapon(int index) {
        if (index < 0 || index >= equipped_weapons.Length) {
            return null;
        }
        return equipped_weapons[index];
    }

    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(activeWeaponIndex);
        bool notSprinting = rigController.GetCurrentAnimatorStateInfo(2).shortNameHash == Animator.StringToHash("not_sprinting");
        bool canFire = !isHolstered && notSprinting && !reload.isReloading;
        if (weapon) {
            if (Input.GetButton("Fire1") && canFire && !weapon.isFiring) {
                weapon.StartFiring();
            }

            if (Input.GetButtonUp("Fire1") || !canFire) {
                weapon.StopFiring();
            }

            weapon.UpdateWeapon(Time.deltaTime, crossHairTarget.position);
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            ToggleActiveWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SetActiveWeapon(WeaponSlot.Secondary);
        }
    }

    public void Equip(RaycastWeapon newWeapon) {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon) {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.recoil.characterAiming = characterAiming;
        weapon.recoil.animator = rigController;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        equipped_weapons[weaponSlotIndex] = weapon;

        SetActiveWeapon(newWeapon.weaponSlot);

        if (ammoWidget) {
            ammoWidget.Refresh(weapon.ammoCount, weapon.clipCount);
        }
    }

    void ToggleActiveWeapon() {
        bool isHolstered = rigController.GetBool("holster_weapon");
        if (isHolstered) {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        } else {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
    }

    void SetActiveWeapon(WeaponSlot weaponSlot) {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;

        if (holsterIndex == activateIndex || isChangingWeapon) {
            return;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex) {
        rigController.SetInteger("weapon_index", activateIndex);
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }

    IEnumerator HolsterWeapon(int index) {
        isChangingWeapon = true;
        isHolstered = true;
        var weapon = GetWeapon(index);
        if (weapon) {
            rigController.SetBool("holster_weapon", true);
            do {
                yield return new WaitForSeconds(0.05f);
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        }
        isChangingWeapon = false;
    }

    IEnumerator ActivateWeapon(int index) {
        isChangingWeapon = true;
        var weapon = GetWeapon(index);
        if (weapon) {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("weapon_" + weapon.weaponName + "_equip");
            do {
                yield return new WaitForSeconds(0.05f);
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
            isHolstered = false;
        }
        isChangingWeapon = false;
    }

    public void DropWeapon() {
        var currentWeapon = GetActiveWeapon();
        if (currentWeapon) {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            equipped_weapons[activeWeaponIndex] = null;
        }
    }

    public void RefillAmmo(int clipCount) {
        var weapon = GetActiveWeapon();
        if (weapon) {
            weapon.clipCount += clipCount;
            ammoWidget.Refresh(weapon.ammoCount, weapon.clipCount);
        }
    }
}
